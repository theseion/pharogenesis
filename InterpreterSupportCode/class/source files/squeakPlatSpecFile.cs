squeakPlatSpecFile

	^ '/* sqPlatformSpecific.h -- Platform-specific prototypes and definitions */

/* How to use this file:
   This file is for general platform-specific macros and declarations.
   Function prototypes that are unlikely to introduce name conflicts on
   other platforms can be added directly. Macro re-definitions or conflicting
   function prototypes can be wrapped in a #ifdefs. Alternatively, a customized
   version of this file can be used on that platform. The goal is to keep all
   the other header files generic across platforms. To override a definition or
   macro from sq.h, you must first #undef it, then provide the new definition.
*/

#ifdef UNIX
/* unix-specific prototypes and definitions */
void aioPollForIO(int microSeconds, int extraFd);
#define SQ_FORM_FILENAME	"squeak-form.ppm"

/* undefine clock macros that are implemented as functions */
#undef ioMSecs
#undef ioMicroMSecs
#undef ioLowResMSecs
#endif /* UNIX */

#ifdef macintosh
/* replace the image file manipulation macros with functions */
#undef sqImageFile
#undef sqImageFileClose
#undef sqImageFileOpen
#undef sqImageFilePosition
#undef sqImageFileRead
#undef sqImageFileSeek
#undef sqImageFileWrite
#undef sqAllocateMemory

typedef int sqImageFile;
void        sqImageFileClose(sqImageFile f);
sqImageFile sqImageFileOpen(char *fileName, char *mode);
int         sqImageFilePosition(sqImageFile f);
int         sqImageFileRead(void *ptr, int elementSize, int count, sqImageFile f);
void        sqImageFileSeek(sqImageFile f, int pos);
int         sqImageFileWrite(void *ptr, int elementSize, int count, sqImageFile f);
void *						sqAllocateMemory(int minHeapSize, int desiredHeapSize);

/* override reserveExtraCHeapBytes() macro to reduce Squeak object heap size on Mac */
#undef reserveExtraCHeapBytes
#define reserveExtraCHeapBytes(origHeapSize, bytesToReserve) (origHeapSize - bytesToReserve)

/* undefine clock macros that are implemented as functions */
#undef ioMSecs
#undef ioMicroMSecs

/* macro to return from interpret() loop in browser plugin VM */
#define ReturnFromInterpret() return
#endif /* macintosh */

/* prototypes missing from CW11 headers */
#include <textutils.h>
void CopyPascalStringToC(ConstStr255Param src, char* dst);
void CopyCStringToPascal(const char* src, Str255 dst);


#ifdef ACORN
/* acorn memory allocation */
#undef sqAllocateMemory
#define sqAllocateMemory(minHeapSize, desiredHeapSize) platAllocateMemory(desiredHeapSize)
#undef sqFilenameFromString
#define sqFilenameFromString(dst, src, num) sqFilenameFromString(dst, src, num)

/* string copying macro to compensate for bug in Acorn library code */
#define copyNCharsFromTo(num, src, dst)\
if(1) {int sqfni;\
	char cc;\
	for (sqfni = 0; sqfni < num; sqfni++) {\
		dst[sqfni] = cc = *((char *) (src + sqfni));\
		if ( cc == 0) break;\
	}\
	dst[num] = 0;\
}

/* undefine clock macros that are implemented as functions */
#undef ioMicroMSecs
#undef ioMSecs
#define ioMSecs() (10* (int)os_read_monotonic_time())
#undef ioLowResMSecs
#define ioLowResMSecs() (ioMSecs())
#endif /* ACORN */

#ifdef WIN32
/* Override necessary definitions */
#undef putchar
#include "sqWin32Alloc.h"

#ifdef WIN32_FILE_SUPPORT

#undef sqImageFile
#undef sqImageFileClose
#undef sqImageFileOpen
#undef sqImageFilePosition
#undef sqImageFileRead
#undef sqImageFileSeek
#undef sqImageFileWrite

#define sqImageFile unsigned long
int sqImageFileClose(sqImageFile h);
sqImageFile sqImageFileOpen(char *fileName, char *mode);
int sqImageFilePosition(sqImageFile h);
int sqImageFileRead(void *ptr, int sz, int count, sqImageFile h);
int sqImageFileSeek(sqImageFile h, int pos);
int sqImageFileWrite(void *ptr, int sz, int count, sqImageFile h);

#endif /* WIN32_FILE_SUPPORT */

/* pluggable primitive support */
#if defined(_MSC_VER) || defined(__MINGW32__)
#  undef EXPORT
#  define EXPORT(returnType) __declspec( dllexport ) returnType
#endif 

/* undefine clock macros that are implemented as functions */
#undef ioMSecs
#undef ioLowResMSecs
#undef ioMicroMSecs

/* Declare GetTickCount() in case <windows.h> is not included */
#if !defined(_WINDOWS_) && !defined(_WIN32_WCE) && !defined(_WINDOWS_H)
__declspec(dllimport) unsigned long __stdcall GetTickCount(void);
#endif

#define ioLowResMSecs() GetTickCount()

#endif /* WIN32 */

'