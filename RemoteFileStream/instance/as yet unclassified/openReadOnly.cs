openReadOnly
	"If we have data, don't reread."

	self readOnly.
	readLimit _ readLimit max: position.
	localDataValid ifFalse: [remoteFile getFileNamed: remoteFile fileName into: self].
		"sets localDataValid _ true"