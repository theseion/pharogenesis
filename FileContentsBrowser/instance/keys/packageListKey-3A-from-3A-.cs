packageListKey: aChar from: view
	aChar == $f ifTrue: [^ self findClass].
	self arrowKey: aChar from: view