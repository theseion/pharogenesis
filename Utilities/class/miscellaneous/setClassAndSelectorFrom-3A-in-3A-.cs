setClassAndSelectorFrom: messageIDString in: csBlock
	"Decode strings of the form <className> [class] <selectorName>.   If <className> does not exist as a class, use nil for the class in the block"

	| aStream aClass maybeClass sel |
	aStream _ ReadStream on: messageIDString.
	aClass _ Smalltalk at: (aStream upTo: $ ) asSymbol ifAbsent: [nil].
	maybeClass _ aStream upTo: $ .
	sel _ aStream upTo: $ .
	((maybeClass = 'class') & (sel size ~= 0))
		ifTrue:
			[aClass
				ifNil:
					[csBlock value: nil value: sel asSymbol]
				ifNotNil:
					[csBlock value: aClass class value: sel asSymbol]]
		ifFalse:
			[csBlock value: aClass value: maybeClass asSymbol]


"
Utilities setClassAndSelectorFrom: 'Utilities class oppositeModeTo:' in: [:aClass :aSelector | Transcript cr; show: 'Class = ', aClass name printString, ' selector = ', aSelector printString].

Utilities setClassAndSelectorFrom: 'MessageSet setClassAndSelectorIn:' in: [:aClass :aSelector | Transcript cr; show: 'Class = ', aClass name printString, ' selector = ', aSelector printString].
"
