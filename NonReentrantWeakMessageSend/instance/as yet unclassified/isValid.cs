isValid
	"Answer the superclass vlaue of isValid or false if
	executing is true."

	^self executing
		ifTrue: [false]
		ifFalse: [super isValid]