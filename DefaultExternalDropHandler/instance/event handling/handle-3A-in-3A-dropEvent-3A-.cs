handle: dropStream in: pasteUp dropEvent: anEvent 
	"the file was just droped, let's do our job"
	| fileName services theOne |
	fileName := dropStream name.
	""
	services := self servicesForFileNamed: fileName.
	""
	"no service, default behavior"
	services isEmpty
		ifTrue: [""
			dropStream edit.
			^ self].
	""
	theOne := self chooseServiceFrom: services.
	theOne isNil

		ifFalse: [theOne performServiceFor: dropStream]