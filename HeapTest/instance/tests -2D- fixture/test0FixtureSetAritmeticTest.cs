test0FixtureSetAritmeticTest
	self 
		shouldnt: [ self collection ]
		raise: Error.
	self deny: self collection isEmpty.
	self 
		shouldnt: [ self nonEmpty ]
		raise: Error.
	self deny: self nonEmpty isEmpty.
	self 
		shouldnt: [ self anotherElementOrAssociationNotIn ]
		raise: Error.
	self collection isDictionary 
		ifTrue: 
			[ self deny: (self collection associations includes: self anotherElementOrAssociationNotIn key) ]
		ifFalse: 
			[ self deny: (self collection includes: self anotherElementOrAssociationNotIn) ].
	self 
		shouldnt: [ self collectionClass ]
		raise: Error