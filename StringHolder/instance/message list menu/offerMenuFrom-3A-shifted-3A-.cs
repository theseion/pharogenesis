offerMenuFrom: menuRetriever shifted: aBoolean
	"Pop up, in morphic or mvc as the case may be, a menu whose target is the receiver and whose contents are provided by sending the menuRetriever to the receiver.  The menuRetriever takes two arguments: a menu, and a boolean representing the shift state."

	| aMenu |
	Smalltalk isMorphic
		ifTrue:
			[aMenu _ MenuMorph new defaultTarget: self.
			self perform: menuRetriever with: aMenu with: aBoolean.
			aMenu popUpInWorld]
		ifFalse:
			[aMenu _ CustomMenu new.
			self perform: menuRetriever with: aMenu with: aBoolean.
			aMenu invokeOn: self]