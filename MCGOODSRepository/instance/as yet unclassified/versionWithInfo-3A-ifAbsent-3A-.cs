versionWithInfo: aVersionInfo ifAbsent: errorBlock
	^ self root at: aVersionInfo ifAbsent: errorBlock