headerFile
^'/* File support definitions */
/* squeak file record; see sqFilePrims.c for details */
typedef struct {
	FILE	*file;
	int		sessionID;
	int		writable;
	int		fileSize;
	int		lastOp;  /* 0 = uncommitted, 1 = read, 2 = write */
} SQFile;

/* file i/o */
int sqFileAtEnd(SQFile *f);
int sqFileClose(SQFile *f);
int sqFileDeleteNameSize(int sqFileNameIndex, int sqFileNameSize);
int sqFileGetPosition(SQFile *f);
int sqFileInit(void);
int sqFileShutdown(void);
int sqFileOpen(SQFile *f, int sqFileNameIndex, int sqFileNameSize, int writeFlag);
int sqFileReadIntoAt(SQFile *f, int count, int byteArrayIndex, int startIndex);
int sqFileRenameOldSizeNewSize(int oldNameIndex, int oldNameSize, int newNameIndex, int newNameSize);
int sqFileSetPosition(SQFile *f, int position);
int sqFileSize(SQFile *f);
int sqFileValid(SQFile *f);
int sqFileWriteFromAt(SQFile *f, int count, int byteArrayIndex, int startIndex);

/* directories */
int dir_Create(char *pathString, int pathStringLength);
int dir_Delete(char *pathString, int pathStringLength);
int dir_Delimitor(void);
int dir_Lookup(char *pathString, int pathStringLength, int index,
	/* outputs: */
	char *name, int *nameLength, int *creationDate, int *modificationDate,
	int *isDirectory, int *sizeIfFile);
int dir_PathToWorkingDir(char *pathName, int pathNameMax);
int dir_SetMacFileTypeAndCreator(char *filename, int filenameSize, char *fType, char *fCreator);
'.