includesVersionNamed: aString
	self repositoriesDo: [:ea | (ea includesVersionNamed: aString) ifTrue: [^ true]].
	^ false	