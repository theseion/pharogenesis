sourceCode
^'/*
	This file contains the low-level support code for OS
	surfaces. Since FXBlt may operate on any of these
	some functions must be provided by the client. It is
	crucial that every client creating surfaces for use
	by Squeak registers the surfaces using the functions
	provided here. Other concrete primitives (such as
	creation or destroy) of surfaces can be handled by
	additional support code, querying the surface handle
	from ioFindSurface but the functions provided below
	are critical for any BitBlt operations.
*/

#include <math.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <time.h>

/* Default EXPORT macro that does nothing (see comment in sq.h): */
#define EXPORT(returnType) returnType

#include "sqVirtualMachine.h"
#include "sqConfig.h"
#include "sqPlatformSpecific.h"
#include "SurfacePlugin.h"

/* The definition below will be replaced by 0/1 when the file is generated */
#if $$SURFACE_PLUGIN_STANDALONE$$
# define XNAME(string) string
#else
# define XNAME(string) SurfacePlugin_##string
#endif

typedef struct SqueakSurface {
	int handle; /* client supplied handle */
	sqSurfaceDispatch *dispatch;
} SqueakSurface;

static SqueakSurface *surfaceArray = NULL;
static int numSurfaces = 0;
static int maxSurfaces = 0;

static const char *moduleName = "SurfacePlugin";
static VirtualMachine *interpreterProxy;
#define FAIL { interpreterProxy->primitiveFail(); return 0; }

#pragma export on
/* module initialization/shutdown */
EXPORT(int) XNAME(setInterpreter)(struct VirtualMachine *interpreterProxy);
EXPORT(const char*) XNAME(getModuleName)(void);
EXPORT(int) XNAME(initialiseModule)(void);
EXPORT(int) XNAME(shutdownModule)(void);

/* critical FXBlt entry points */
EXPORT(int) XNAME(ioGetSurfaceFormat) (int surfaceID, int* width, int* height, int* depth, int* isMSB);
EXPORT(int) XNAME(ioLockSurface) (int surfaceID, int *pitch, int x, int y, int w, int h);
EXPORT(int) XNAME(ioUnlockSurface)(int surfaceID, int x, int y, int w, int h);

/* interpreter entry point */
EXPORT(int) XNAME(ioShowSurface)(int surfaceID, int x, int y, int w, int h);

/* client entry points */
EXPORT(int) XNAME(ioRegisterSurface)(int surfaceHandle, sqSurfaceDispatch *fn, int *surfaceID);
EXPORT(int) XNAME(ioUnregisterSurface)(int surfaceID);
EXPORT(int) XNAME(ioFindSurface)(int surfaceID, sqSurfaceDispatch *fn, int *surfaceHandle);
#pragma export off

/* ioGetSurfaceFormat:
	Return information describing the given surface.
	Return true if successful, false otherwise. */
EXPORT(int) XNAME(ioGetSurfaceFormat) (int surfaceID, int* width, int* height, int* depth, int* isMSB)
{
	SqueakSurface *surface;
	if(surfaceID < 0 || surfaceID >= maxSurfaces) FAIL;
	surface = surfaceArray + surfaceID;
	if(surface->dispatch == NULL) FAIL;
	if(!surface->dispatch->getSurfaceFormat) FAIL;
	return surface->dispatch->getSurfaceFormat(surface->handle, width, height, depth, isMSB);
}

/* ioLockSurface:
	Lock the bits of the surface. 
	Return a pointer to the actual surface bits,
	or NULL on failure. */
EXPORT(int) XNAME(ioLockSurface) (int surfaceID, int *pitch, int x, int y, int w, int h)
{
	SqueakSurface *surface;
	if(surfaceID < 0 || surfaceID >= maxSurfaces) FAIL;
	surface = surfaceArray + surfaceID;
	if(surface->dispatch == NULL) FAIL;
	if(!surface->dispatch->lockSurface) FAIL;
	return surface->dispatch->lockSurface(surface->handle, pitch, x, y, w, h);
}

/* ioUnlockSurface:
	Unlock the bits of the surface. 
	The return value is ignored. */
EXPORT(int) XNAME(ioUnlockSurface)(int surfaceID, int x, int y, int w, int h)
{
	SqueakSurface *surface;
	if(surfaceID < 0 || surfaceID >= maxSurfaces) FAIL;
	surface = surfaceArray + surfaceID;
	if(surface->dispatch == NULL) FAIL;
	if(!surface->dispatch->unlockSurface) FAIL;
	return surface->dispatch->unlockSurface(surface->handle, x, y, w, h);
}

/* ioShowSurface:
	Transfer the bits of a surface to the screen. */
EXPORT(int) XNAME(ioShowSurface)(int surfaceID, int x, int y, int w, int h)
{
	SqueakSurface *surface;
	if(surfaceID < 0 || surfaceID >= maxSurfaces) FAIL;
	surface = surfaceArray + surfaceID;
	if(surface->dispatch == NULL) FAIL;
	if(!surface->dispatch->showSurface) FAIL;
	return surface->dispatch->showSurface(surface->handle, x, y, w, h);
}

/* ioRegisterSurface:
	Register a new surface with the given handle and
	the set of surface functions. The new ID is returned
	in surfaceID. Returns true if successful, false 
	otherwise. */
EXPORT(int) XNAME(ioRegisterSurface)(int surfaceHandle, sqSurfaceDispatch *fn, int *surfaceID)
{
	int index;

	if(!fn) return 0;
	if(fn->majorVersion != 1 && fn->minorVersion != 0) return 0;
	if(numSurfaces == maxSurfaces) {
		maxSurfaces = maxSurfaces * 2 + 10;
		surfaceArray = realloc(surfaceArray, sizeof(SqueakSurface) * maxSurfaces);
		for(index = numSurfaces; index < maxSurfaces; index++){
			surfaceArray[index].handle = 0;
			surfaceArray[index].dispatch = NULL;
		}
		index = numSurfaces;
	} else {
		for(index = 0; index < maxSurfaces; index++)
			if(surfaceArray[index].dispatch == NULL)
				break;
	}
	if(index >= maxSurfaces) return 0; /* should not happen */
	surfaceArray[index].handle = surfaceHandle;
	surfaceArray[index].dispatch = fn;
	*surfaceID = index;
	numSurfaces++;
	return 1;
}

/* ioUnregisterSurface:
	Unregister the surface with the given handle.
	Returns true if successful, false otherwise. */
EXPORT(int) XNAME(ioUnregisterSurface)(int surfaceID)
{
	SqueakSurface *surface;
	if(surfaceID < 0 || surfaceID >= maxSurfaces) return 0;
	surface = surfaceArray + surfaceID;
	if(surface->dispatch == NULL) return 0;
	surface->handle = 0;
	surface->dispatch = NULL;
	numSurfaces--;
	return 1;
}

/* ioFindSurface:
	Find the surface with the given ID, and, optionally,
	the given set of surface functions. The registered handle
	is returned in surfaceHandle. Return true if successful
	(e.g., the surface has been found), false otherwise. */
EXPORT(int) XNAME(ioFindSurface)(int surfaceID, sqSurfaceDispatch *fn, int *surfaceHandle)
{
	SqueakSurface *surface;
	if(surfaceID < 0 || surfaceID >= maxSurfaces) return 0;
	surface = surfaceArray + surfaceID;
	if(surface->dispatch == NULL) return 0;
	if(fn && fn != surface->dispatch) return 0;
	*surfaceHandle = surface->handle;
	return 1;
}

EXPORT(int) XNAME(setInterpreter)(struct VirtualMachine* anInterpreter) {
    int ok;

	interpreterProxy = anInterpreter;
	ok = interpreterProxy->majorVersion() == VM_PROXY_MAJOR;
	if (ok == 0) {
		return 0;
	}
	ok = interpreterProxy->minorVersion() >= VM_PROXY_MINOR;
	return ok;
}
EXPORT(const char*) XNAME(getModuleName)(void) {
	return moduleName;
}

EXPORT(int) XNAME(initialiseModule)() {
	surfaceArray = NULL;
	numSurfaces = 0;
	maxSurfaces = 0;
	return 1;
}

EXPORT(int) XNAME(shutdownModule)() {
	/* This module can only be shut down if no surfaces are registered */
	if(numSurfaces != 0) return 0;
	free(surfaceArray);
	return 1;
}
'