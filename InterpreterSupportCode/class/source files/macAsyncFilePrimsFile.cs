macAsyncFilePrimsFile

	^ '/* Adjustments for pluginized VM
 *
 * Note: The Mac support files have not yet been fully converted to
 * pluginization. For the time being, it is assumed that they are linked
 * with the VM. When conversion is complete, they will no longer import
 * "sq.h" and they will access all VM functions and variables through
 * the interpreterProxy mechanism.
 */

#include "sq.h"
#include "AsynchFilePlugin.h"
#include <devices.h>

#if !TARGET_API_MAC_CARBON
#define DisposeIOCompletionUPP(userUPP) DisposeRoutineDescriptor(userUPP)
#endif
/* initialize/shutdown */
int asyncFileInit() { return true; }
int asyncFileShutdown() {} 

/* End of adjustments for pluginized VM */

/*
  Experimental support for asynchronous file reading and writing.

  When a read or write operation is initiated, control is returned to Squeak
  immediately. A semaphore is signaled when the operation completes, at which
  time the client can find out how many bytes were actually read or written
  and copy the results of the read operation from the file buffer into a Squeak
  buffer. Only one operation may be in progress on a given file at a given time,
  but operations on different files may be done in parallel.

  The semaphore is signalled once for each transfer operation that is successfully
  started, even if that operation later fails. Write operations always write
  their entire buffer if they succeed, but read operations may transfer less than
  their buffer size if they are started less than a buffer''s size from the end
  of the file.
  
  The state of a file is kept in the following structure, which is stored directly
  in a Squeak ByteArray object:

    typedef struct {
	  int				sessionID;
	  AsyncFileState	*state;
    } AsyncFile;

  The session ID is used to detect stale files--files that were open
  when the image was saved. The state pointer of such files is meaningless.
  Async file handles use the same session ID as ordinary file handles.

  Note: These primitives are experimental! They need not be implemented on
  every platform, and they may be withdrawn or replaced in a future release.
*/

#include <Errors.h>
#include <Files.h>
#include <Strings.h>

/* Async file handle (defined in header file):
*/

typedef struct {
	ParamBlockRec pb;  /* must be first */
	long	refNum;
	int		writable;
	int		semaIndex;
	int		status;
	int		bytesTransferred;
	int		bufferSize;
	char 	*bufferPtr;
} AsyncFileState;

/*** Status Values ***/
#define IDLE			0
#define LAST_OP_FAILED	1
#define BUSY			2

/*** Imported Variables ***/
extern int successFlag;
extern int thisSession;

/*** Local Vaiables ***/
IOCompletionUPP asyncFileCompletionProc = nil;

/*** Exported Functions ***/
int asyncFileClose(AsyncFile *f);
int asyncFileOpen(AsyncFile *f, int fileNamePtr, int fileNameSize, int writeFlag, int semaIndex);
int asyncFileRecordSize();
int asyncFileReadResult(AsyncFile *f, int bufferPtr, int bufferSize);
int asyncFileReadStart(AsyncFile *f, int fPosition, int count);
int asyncFileWriteResult(AsyncFile *f);
int asyncFileWriteStart(AsyncFile *f, int fPosition, int bufferPtr, int bufferSize);

/*** Local Functions ***/
int asyncFileAllocateBuffer(AsyncFileState *state, int byteCount);
pascal void asyncFileCompletionRoutine(AsyncFileState *state);
int asyncFileInitPB(AsyncFileState *state, int fPosition);
int asyncFileValid(AsyncFile *f);

int asyncFileAllocateBuffer(AsyncFileState *state, int byteCount) {
  /* Allocate a new buffer of the given size if necessary. If the current buffer
	 is already allocated and of the desired size, do nothing. */

	if ((state->bufferPtr != nil) && (state->bufferSize == byteCount)) {
		return;  /* buffer is already allocated and of the desired size */
	}

	/* free old buffer, if any */
	if (state->bufferPtr != nil) {
		DisposePtr(state->bufferPtr);
		state->bufferSize = 0;
		state->bufferPtr = nil;
	}

	/* allocate new buffer */
	state->bufferPtr = NewPtr(byteCount);
	if (state->bufferPtr == nil) {
		state->bufferSize = 0;
		return success(false);  /* could not allocate a buffer of size count */
	}
	state->bufferSize = byteCount;
}

pascal void asyncFileCompletionRoutine(AsyncFileState *state) {
  /* Called when an I/O request completes. Decides what to do based on the given state.
	 Note that the first part of the state record is the I/O parameter block. */

	OSErr err;

	err = state->pb.ioParam.ioResult;
	if ((err != noErr) && (err != eofErr)) {
		/* Note: eofErr indicates that fewer than the count bytes were transfered when
		   reading because the end-of-file was encountered first; it isn''t a real error. */
		state->status = LAST_OP_FAILED;
		state->bytesTransferred = 0;
		signalSemaphoreWithIndex(state->semaIndex);
		return;
	}
	state->bytesTransferred = state->pb.ioParam.ioActCount;
	state->status = IDLE;
	signalSemaphoreWithIndex(state->semaIndex);
}

int asyncFileInitPB(AsyncFileState *state, int fPosition) {
	memset(&state->pb, 0, sizeof(ParamBlockRec));
	state->pb.ioParam.ioCompletion = asyncFileCompletionProc;
	state->pb.ioParam.ioRefNum = state->refNum;
	state->pb.ioParam.ioBuffer = state->bufferPtr;
	state->pb.ioParam.ioReqCount = state->bufferSize;
	state->pb.ioParam.ioPosMode = fsFromStart;
	state->pb.ioParam.ioPosOffset = (fPosition < 0) ? 0 : fPosition;
	state->status = BUSY;
	state->bytesTransferred = 0;
}

int asyncFileValid(AsyncFile *f) {
	return (
		(f != NULL) &&
		(f->sessionID == thisSession) &&
		(f->state != NULL) &&
		(((AsyncFileState *) f->state)->refNum != 0));
}

/*** Exported Functions ***/

int asyncFileClose(AsyncFile *f) {
  /* Close the given asynchronous file. */

	AsyncFileState *state;
	short int volRefNum;
	OSErr err;

	if (!asyncFileValid(f)) return;  /* already closed */
	state = f->state;

	err = GetVRefNum(state->refNum, &volRefNum);
	success(err == noErr);

	err = FSClose(state->refNum);
	success(err == noErr);

	if (successFlag) err = FlushVol(NULL, volRefNum);
	success(err == noErr);

    if (asyncFileCompletionProc != nil)
        DisposeIOCompletionUPP(asyncFileCompletionProc);
  
	asyncFileCompletionProc = nil;
	if (state->bufferPtr != nil) DisposePtr(state->bufferPtr);
	DisposePtr((void *) f->state);
	f->state = nil;
	f->sessionID = 0;
}

int asyncFileOpen(AsyncFile *f, int fileNamePtr, int fileNameSize, int writeFlag, int semaIndex) {
  /* Opens the given file using the supplied AsyncFile structure to record
	 its state. Fails with no side effects if f is already open. Files are
	 always opened in binary mode. */

	int i;
	Str255 cFileName;
	short int fileRefNum;
	AsyncFileState *state;
	OSErr err;

	/* don''t open an already open file */
	if (asyncFileValid(f)) return success(false);

	/* build complete routine descriptor, if necessary */
	if (asyncFileCompletionProc == nil) {
		asyncFileCompletionProc = NewIOCompletionProc((pascal void (*) (union ParamBlockRec *) )asyncFileCompletionRoutine);
	}

	/* copy the file name into a null-terminated C string */
	if (fileNameSize > 255) return success(false);
	
	sqFilenameFromString(cFileName, fileNamePtr, fileNameSize);

	if (!plugInAllowAccessToFilePath((char *) cFileName, fileNameSize)) {
		return success(false);
	}

	CopyCStringToPascal((const char *)cFileName,cFileName);
	f->sessionID = 0;
	if (writeFlag) {
		/* first try to open an existing file read/write: */
		err = HOpenDF(0,0,cFileName, fsRdWrPerm, &fileRefNum); 
		if (err != noErr) {
			/* file does not exist; must create it. */
			err = HCreate(0, 0, cFileName,''R*ch'',''TEXT''); 
			if (err != noErr) return success(false);
			err = HOpenDF(0,0,cFileName,fsRdWrPerm, &fileRefNum);
			if (err != noErr) return success(false);
		}
	} else {
		/* open the file read-only  */
		err = HOpenDF(0,0,cFileName, fsRdPerm, &fileRefNum); 
		if (err != noErr) return success(false);
	}
	f->state = (AsyncFileState *) NewPtr(sizeof(AsyncFileState));	/* allocate state record */
	if (f->state == nil) {
		FSClose(fileRefNum);
		return success(false);
	}
	f->sessionID = thisSession;
	state = (AsyncFileState *) f->state;
	state->refNum = fileRefNum;
	state->writable = writeFlag;
	state->semaIndex = semaIndex;
	state->status = IDLE;
	state->bytesTransferred = 0;
	state->bufferSize = 0;
	state->bufferPtr = nil;
}

int asyncFileReadResult(AsyncFile *f, int bufferPtr, int bufferSize) {
  /* Copy up to bufferSize bytes from the buffer of the last read operation
	 into the given Squeak buffer, and return the number of bytes copied.
	 Negative values indicate:
		-1    -- busy; the last operation has not finished yet
		-2    -- error; the last operation failed
	Note that a read operation may read fewer bytes than requested if, for
	example, there are fewer than the requested number of bytes between the
	starting file position of the read operation and the end-of-file. */

	AsyncFileState *state;
	int bytesRead;

	if (!asyncFileValid(f)) return success(false);
	state = f->state;
	if (state->status == BUSY) return -1;
	if (state->status == LAST_OP_FAILED) return -2;

	/* copy the file buffer into the squeak buffer */
	bytesRead = (bufferSize < state->bytesTransferred) ? bufferSize : state->bytesTransferred;
	memcpy((char *) bufferPtr, state->bufferPtr, bytesRead);
	return bytesRead;
}

int asyncFileReadStart(AsyncFile *f, int fPosition, int count) {
  /* Start an asynchronous operation to read count bytes from the given file
	 starting at the given file position. The file''s semaphore will be signalled when
	 the operation is complete. The client may then use asyncFileReadResult() to
	 find out if the operation succeeded and to get the data that was read. */

	AsyncFileState *state;
	OSErr err;

	if (!asyncFileValid(f)) return success(false);
	state = f->state;
	if (state->status == BUSY) return success(false);  /* operation in progress */

	/* allocate a new buffer if necessary */
	asyncFileAllocateBuffer(state, count);
	if (state->bufferPtr == nil) return success(false);  /* could not allocate buffer */

	asyncFileInitPB(state, fPosition);
	err = PBReadAsync(&state->pb);
	if (err != noErr) {
		state->status = IDLE;
		return success(false);
	}
}

int asyncFileRecordSize() {
	return sizeof(AsyncFile);
}

int asyncFileWriteResult(AsyncFile *f) {
  /* Return the number of bytes copied by the last write operation.
	 Negative values indicate:
		-1    -- busy; the last operation has not finished yet
		-2    -- error; the last operation failed */

	AsyncFileState *state;

	if (!asyncFileValid(f)) return success(false);
	state = f->state;
	if (state->status == BUSY) return -1;
	if (state->status == LAST_OP_FAILED) return -2;
	return state->bytesTransferred;
}

int asyncFileWriteStart(AsyncFile *f, int fPosition, int bufferPtr, int bufferSize) {
  /* Start an asynchronous operation to write bufferSize bytes to the given file
	 starting at the given file position. The file''s semaphore will be signalled when
	 the operation is complete. The client may then use asyncFileWriteResult() to
	 find out if the operation succeeded and how many bytes were actually written. */

	AsyncFileState *state;
	OSErr err;

	if (!asyncFileValid(f)) return success(false);
	state = f->state;
	if (state->status == BUSY) return success(false);  /* operation in progress */
	if (!state->writable) return success(false);

	/* allocate a new buffer if necessary */
	asyncFileAllocateBuffer(state, bufferSize);
	if (state->bufferPtr == nil) return success(false);  /* could not allocate buffer */

	/* copy the squeak buffer into the file buffer */
	memcpy(state->bufferPtr, (char *) bufferPtr, bufferSize);

	asyncFileInitPB(state, fPosition);
	err = PBWriteAsync(&state->pb);
	if (err != noErr) {
		state->status = IDLE;
		return success(false);
	}
}
'