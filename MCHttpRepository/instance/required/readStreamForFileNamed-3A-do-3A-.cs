readStreamForFileNamed: aString do: aBlock
	| contents |
	contents := HTTPSocket httpGet: (self urlForFileNamed: aString) args: nil user: self user passwd: self password.
	^ contents isString ifFalse: [aBlock value: contents]