browseRecent: charCount 
	"ChangeList browseRecent: 5000"
	"Opens a changeList on the end of the changes log file"
	^ self browseRecent: charCount on: (SourceFiles at: 2) 