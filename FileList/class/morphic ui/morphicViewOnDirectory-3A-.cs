morphicViewOnDirectory: aFileDirectory
	| aFileList window fileListBottom midLine fileListTopOffset buttonPane |

	aFileList := self new directory: aFileDirectory.
	window := (SystemWindow labelled: aFileDirectory pathName) model: aFileList.

	fileListTopOffset := (TextStyle defaultFont pointSize * 2) + 14.
	fileListBottom := 0.4.
	midLine := 0.4.
	buttonPane := aFileList optionalButtonRow addMorph:
		(aFileList morphicPatternPane vResizing: #spaceFill; yourself).
	self addFullPanesTo: window from: {
		{buttonPane. 0@0 corner: 1@0. 0@0 corner: 0@fileListTopOffset}.
		{aFileList morphicDirectoryTreePane. 0@0 corner: midLine@fileListBottom. 
					0@fileListTopOffset corner: 0@0}.
		{aFileList morphicFileListPane. midLine @ 0 corner: 1@fileListBottom. 
					0@fileListTopOffset corner: 0@0}.
		{aFileList morphicFileContentsPane. 0@fileListBottom corner: 1@1. nil}.
	}.
	aFileList postOpen.
	^ window 