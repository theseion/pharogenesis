squeakHeaderFile

	^ '#include <math.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <time.h>

#include "sqConfig.h"
#include "sqVirtualMachine.h"

#define true 1
#define false 0
#define null 0  /* using "null" because nil is predefined in Think C */

/* pluggable primitives macros */
/* Note: All pluggable primitives are defined as
	EXPORT(int) somePrimitive(void)
   If the platform requires special declaration modifiers
   the EXPORT macro can be redefined
*/
#define EXPORT(returnType) returnType

/* image save/restore macros */
/* Note: The image file save and restore code uses these macros; they
   can be redefined in sqPlatformSpecific.h if desired. These default
   versions are defined in terms of the ANSI Standard C libraries.
*/
#define sqImageFile FILE *
#define sqImageFileClose(f)                  fclose(f)
#define sqImageFileOpen(fileName, mode)      fopen(fileName, mode)
#define sqImageFilePosition(f)               ftell(f)
#define sqImageFileRead(ptr, sz, count, f)   fread(ptr, sz, count, f)
#define sqImageFileSeek(f, pos)              fseek(f, pos, SEEK_SET)
#define sqImageFileWrite(ptr, sz, count, f)  fwrite(ptr, sz, count, f)
#define sqAllocateMemory(minHeapSize, desiredHeapSize)   malloc(desiredHeapSize)

/* platform-dependent float conversion macros */
/* Note: Second argument must be a variable name, not an expression! */
/* Note: Floats in image are always in PowerPC word order; change
   these macros to swap words if necessary. This costs no extra and
   obviates sometimes having to word-swap floats when reading an image.
*/
#if defined(DOUBLE_WORD_ALIGNMENT) || defined(DOUBLE_WORD_ORDER)
# ifdef DOUBLE_WORD_ORDER
/* word-based copy with swapping for non-PowerPC order */
#   define storeFloatAtfrom(i, floatVarName) \
	*((int *) (i) + 0) = *((int *) &(floatVarName) + 1); \
	*((int *) (i) + 1) = *((int *) &(floatVarName) + 0);
#   define fetchFloatAtinto(i, floatVarName) \
	*((int *) &(floatVarName) + 0) = *((int *) (i) + 1); \
	*((int *) &(floatVarName) + 1) = *((int *) (i) + 0);
# else /*!DOUBLE_WORD_ORDER*/
/* word-based copy for machines with alignment restrictions */
#   define storeFloatAtfrom(i, floatVarName) \
	*((int *) (i) + 0) = *((int *) &(floatVarName) + 0); \
	*((int *) (i) + 1) = *((int *) &(floatVarName) + 1);
#   define fetchFloatAtinto(i, floatVarName) \
	*((int *) &(floatVarName) + 0) = *((int *) (i) + 0); \
	*((int *) &(floatVarName) + 1) = *((int *) (i) + 1);
# endif /*!DOUBLE_WORD_ORDER*/
#else /*!(DOUBLE_WORD_ORDER||DOUBLE_WORD_ALIGNMENT)*/
/* for machines that allow doubles to be on any word boundary */
# define storeFloatAtfrom(i, floatVarName) \
	*((double *) (i)) = (floatVarName);
# define fetchFloatAtinto(i, floatVarName) \
	(floatVarName) = *((double *) (i));
#endif

/* platform-dependent memory size adjustment macro */
/* Note: This macro can be redefined to allows platforms with a
   fixed application memory partition (notably, the Macintosh)
   to reserve extra C heap memory for special applications that need
   it (e.g., for a 3D graphics library). Since most platforms can
   extend their application memory partition at run time if needed,
   this macro is defined as a noop here and redefined if necessary
   in sqPlatformSpecific.h.
*/

#define reserveExtraCHeapBytes(origHeapSize, bytesToReserve) origHeapSize

/* platform-dependent millisecond clock macros */
/* Note: The Squeak VM uses three different clocks functions for
   timing. The primary one, ioMSecs(), is used to implement Delay
   and Time millisecondClockValue. The resolution of this clock
   determines the resolution of these basic timing functions. For
   doing real-time control of music and MIDI, a clock with resolution
   down to one millisecond is preferred, but a coarser clock (say,
   1/60th second) can be used in a pinch. The VM calls a different
   clock function, ioLowResMSecs(), in order to detect long-running
   primitives. This function must be inexpensive to call because when
   a Delay is active it is polled twice per primitive call. On several
   platforms (Mac, Win32), the high-resolution system clock used in
   ioMSecs() would incur enough overhead in this case to slow down the
   the VM significantly. Thus, a cheaper clock with low resolution is
   used to implement ioLowResMSecs() on these platforms. Finally, the
   function ioMicroMSecs() is used only to collect timing statistics
   for the garbage collector and other VM facilities. (The function
   name is meant to suggest that the function is based on a clock
   with microsecond accuracy, even though the times it returns are
   in units of milliseconds.) This clock must have enough precision to
   provide accurate timings, and normally isn''t called frequently
   enough to slow down the VM. Thus, it can use a more expensive clock
   that ioMSecs(). By default, all three clock functions are defined
   here as macros based on the standard C library function clock().
   Any of these macros can be overridden in sqPlatformSpecific.h.
*/

int ioMSecs(void);
int ioLowResMSecs(void);
int ioMicroMSecs(void);

#define ioMSecs()		((1000 * clock()) / CLOCKS_PER_SEC)
#define ioLowResMSecs()	((1000 * clock()) / CLOCKS_PER_SEC)
#define ioMicroMSecs()	((1000 * clock()) / CLOCKS_PER_SEC)

/* filename copy/transform macro. An opportunity to transform the filenames for
   platforms with strange needs, anda simple encapsulation for everyone else
*/
#define sqFilenameFromString(dst, src, num) \
if (1) {\
	int i; \
	for (i = 0; i < num; i++) { \
		dst[i] = *((char *) (src + i)); \
	} \
	dst[num] = 0;\
}

/* this include file may redefine earlier definitions and macros: */
#include "sqPlatformSpecific.h"

/* interpreter entry points */
void error(char *s);
int checkedByteAt(int byteAddress);
int checkedByteAtput(int byteAddress, int byte);
int checkedLongAt(int byteAddress);
int checkedLongAtput(int byteAddress, int a32BitInteger);
int fullDisplayUpdate(void);
int initializeInterpreter(int bytesToShift);
int interpret(void);
int primitiveFail(void);
int signalSemaphoreWithIndex(int index);
int success(int);

/* display, mouse, keyboard, time i/o */
int ioBeep(void);
int ioExit(void);
int ioForceDisplayUpdate(void);
int ioFormPrint(
	int bitsAddr, int width, int height, int depth,
	double hScale, double vScale, int landscapeFlag);
int ioSetFullScreen(int fullScreen);
int ioRelinquishProcessorForMicroseconds(int microSeconds);
int ioScreenSize(void);
int ioSeconds(void);
int ioSetCursor(int cursorBitsIndex, int offsetX, int offsetY);
int ioSetCursorWithMask(int cursorBitsIndex, int cursorMaskIndex, int offsetX, int offsetY);
int ioShowDisplay(
	int dispBitsIndex, int width, int height, int depth,
	int affectedL, int affectedR, int affectedT, int affectedB);
int ioHasDisplayDepth(int depth);
int ioSetDisplayMode(int width, int height, int depth, int fullscreenFlag);

/* User input recording I:
   In general, either set of input function can be supported,
   depending on the platform. This (first) set is state based
   and should be supported even on platforms that make use
   of the newer event driven API to support older images 
   without event support.
*/
int ioGetButtonState(void);
int ioGetKeystroke(void);
int ioMousePoint(void);
int ioPeekKeystroke(void);
/* Note: In an event driven architecture, ioProcessEvents is obsolete.
   It can be implemented as a no-op since the image will check for
   events in regular intervals. */
int ioProcessEvents(void);


/* User input recording II:
   The following functions and definition can be used on
   platform supporting events directly.
*/
/* types of events */
#define EventTypeNone 0
#define EventTypeMouse 1
#define EventTypeKeyboard 2

/* keypress state for keyboard events */
#define EventKeyChar 0
#define EventKeyDown 1
#define EventKeyUp 2

/* button definitions */
#define RedButtonBit 4
#define BlueButtonBit 2
#define YellowButtonBit 1

/* modifier definitions */
#define ShiftKeyBit 1
#define CtrlKeyBit 2
#define OptionKeyBit 4
#define CommandKeyBit 8

/* generic input event definition */
typedef struct sqInputEvent {
	int type; /* type of event; either one of EventTypeXXX */
	unsigned int timeStamp; /* time stamp */
     /* the interpretation of the following fields depend on the type of the event */
	int unused1;
	int unused2;
	int unused3;
	int unused4;
	int unused5;
	int unused6;
} sqInputEvent;

/* mouse input event definition */
typedef struct sqMouseEvent {
	int type; /* EventTypeMouse */
	unsigned int timeStamp; /* time stamp */
	int x; /* mouse position x */
	int y; /* mouse position y */
	int buttons; /* combination of xxxButtonBit */
	int modifiers; /* combination of xxxKeyBit */
	int reserved1; /* reserved for future use */
	int reserved2; /* reserved for future use */
} sqMouseEvent;

/* keyboard input event definition */
typedef struct sqKeyboardEvent {
	int type; /* EventTypeKeyboard */
	unsigned int timeStamp; /* time stamp */
	int charCode; /* character code in Mac Roman encoding */
	int pressCode; /* press code; any of EventKeyXXX */
	int modifiers; /* combination of xxxKeyBit */
	int reserved1; /* reserved for future use */
	int reserved2; /* reserved for future use */
	int reserved3; /* reserved for future use */
} sqKeyboardEvent;

/* set an asynchronous input semaphore index for events */
int ioSetInputSemaphore(int semaIndex);
/* retrieve the next input event from the OS */
int ioGetNextEvent(sqInputEvent *evt);

/* image file and VM path names */
extern char imageName[];
int imageNameGetLength(int sqImageNameIndex, int length);
int imageNamePutLength(int sqImageNameIndex, int length);
int imageNameSize(void);
int vmPathSize(void);
int vmPathGetLength(int sqVMPathIndex, int length);

/* save/restore */
/* Read the image from the given file starting at the given image offset */
int readImageFromFileHeapSizeStartingAt(sqImageFile f, int desiredHeapSize, int imageOffset);
/* NOTE: The following is obsolete - it is only provided for compatibility */
#define readImageFromFileHeapSize(f, s) readImageFromFileHeapSizeStartingAt(f,s,0)

/* clipboard (cut/copy/paste) */
int clipboardSize(void);
int clipboardReadIntoAt(int count, int byteArrayIndex, int startIndex);
int clipboardWriteFromAt(int count, int byteArrayIndex, int startIndex);

/* browser plug-in support */
int plugInAllowAccessToFilePath(char *pathString, int pathStringLength);
void plugInForceTimeToReturn(void);
int plugInInit(char *imageName);
int plugInNotifyUser(char *msg);
void plugInSetStartTime(void);
int plugInShutdown(void);
int plugInTimeToReturn(void);

/* interpreter entry points needed by compiled primitives */
void * arrayValueOf(int arrayOop);
int checkedIntegerValueOf(int intOop);
void * fetchArrayofObject(int fieldIndex, int objectPointer);
double fetchFloatofObject(int fieldIndex, int objectPointer);
int fetchIntegerofObject(int fieldIndex, int objectPointer);
double floatValueOf(int floatOop);
int pop(int nItems);
int pushInteger(int integerValue);
int sizeOfSTArrayFromCPrimitive(void *cPtr);
int storeIntegerofObjectwithValue(int fieldIndex, int objectPointer, int integerValue);

/* profiling */
int clearProfile(void);
int dumpProfile(void);
int startProfiling(void);
int stopProfiling(void);

/* system attributes */
int attributeSize(int id);
int getAttributeIntoLength(int id, int byteArrayIndex, int length);

/* ar 5/13/2000:
	The following set of miscellaneous and sound primitives should
	at some point go into named primitives. Right now there are a few
	problems with that (related to how the distinct set of methods can
	be defined as residing in one plugin).
*/
/* miscellaneous primitives */
int primBitmapcompresstoByteArray(void);
int primBitmapdecompressfromByteArrayat(void);
int primSampledSoundconvert8bitSignedFromto16Bit(void);
int primStringcomparewithcollated(void);
int primStringfindFirstInStringinSetstartingAt(void);
int primStringfindSubstringinstartingAtmatchTable(void);
int primStringindexOfAsciiinStringstartingAt(void);
int primStringtranslatefromtotable(void);

/* sound generation primitives (old, for backward compatibility) */
int primWaveTableSoundmixSampleCountintostartingAtpan(void);
int primFMSoundmixSampleCountintostartingAtpan(void);
int primPluckedSoundmixSampleCountintostartingAtpan(void);
int primSampledSoundmixSampleCountintostartingAtpan(void);
int oldprimSampledSoundmixSampleCountintostartingAtleftVolrightVol(void);

/* sound generation primitives */
int primFMSoundmixSampleCountintostartingAtleftVolrightVol(void);
int primLoopedSampledSoundmixSampleCountintostartingAtleftVolrightVol(void);
int primPluckedSoundmixSampleCountintostartingAtleftVolrightVol(void);
int primReverbSoundapplyReverbTostartingAtcount(void);
int primSampledSoundmixSampleCountintostartingAtleftVolrightVol(void);

/*** sound compression primitives ***/
int primADPCMCodecprivateDecodeMono(void);
int primADPCMCodecprivateDecodeStereo(void);
int primADPCMCodecprivateEncodeMono(void);
int primADPCMCodecprivateEncodeStereo(void);


/*** pluggable primitive support ***/
/* NOTE: The following functions are those implemented by sqNamedPrims.c */
int ioLoadExternalFunctionOfLengthFromModuleOfLength(
  int functionNameIndex, int functionNameLength,
  int moduleNameIndex, int moduleNameLength);
int ioUnloadModuleOfLength(int moduleNameIndex, int moduleNameLength);
int ioLoadFunctionFrom(char *functionName, char *pluginName);
int ioShutdownAllModules(void);
int ioUnloadModule(char *);
int ioUnloadModuleOfLength(int moduleNameIndex, int moduleNameLength);
char *ioListBuiltinModule(int moduleIndex);
char *ioListLoadedModule(int moduleIndex);
/* The next two are FFI entries! (implemented in sqNamedPrims.c as well) */
int ioLoadModuleOfLength(int moduleNameIndex, int moduleNameLength);
int ioLoadSymbolOfLengthFromModule(int functionNameIndex, int functionNameLength, int moduleHandle);

/* The next three functions must be implemented by sqXYZExternalPrims.c */
/* ioLoadModule:
	Load a module from disk.
	WARNING: this always loads a *new* module. Don''t even attempt to find a loaded one.
	WARNING: never primitiveFail() within, just return 0
*/
int ioLoadModule(char *pluginName);

/* ioFindExternalFunctionIn:
	Find the function with the given name in the moduleHandle.
	WARNING: never primitiveFail() within, just return 0.
*/
int ioFindExternalFunctionIn(char *lookupName, int moduleHandle);

/* ioFreeModule:
	Free the module with the associated handle.
	WARNING: never primitiveFail() within, just return 0.
*/
int ioFreeModule(int moduleHandle);

/* The Squeak version this interpreter was generated from */
extern const char *interpreterVersion;
'