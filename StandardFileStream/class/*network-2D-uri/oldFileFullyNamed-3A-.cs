oldFileFullyNamed: t2 

	^ (self isAFileNamed: t2)
		ifTrue: [self new open: t2 forWrite: true]
		ifFalse: [(FileDoesNotExistException fileName: t2) signal]