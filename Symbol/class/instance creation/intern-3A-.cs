intern: aString 
	"Answer a unique Symbol whose characters are those of aString."

	| ascii table mainTable index sym numArgs symbol lastNilIndex |

	aString size = 0
		ifTrue: [ascii _ 0]
		ifFalse:
			[ascii _ (aString at: 1) asciiValue.
			aString size = 1 ifTrue: [ascii < 128 ifTrue: 
				[^ SingleCharSymbols at: ascii + 1]]].

	table _ ((ascii >= "$a asciiValue" 97) and:
		[(ascii <= "$z asciiValue" 122) and:
		[(numArgs _ aString numArgs) >= 0]])
			ifTrue: [(mainTable _ SelectorTables
									at: (numArgs + 1 min: SelectorTables size))
						at: (index _ ascii - "($a asciiValue - 1)" 96)]
			ifFalse: [(mainTable _ OtherTable)
						at: (index _ aString stringhash \\ OtherTable size + 1)].

	1 to: table size do: [:i |
		symbol _ table at: i.
		symbol isNil 
			ifTrue:[lastNilIndex _ i]
			ifFalse:[(aString size = symbol size and:[aString = symbol])
						ifTrue:[^symbol]]
	].

	sym _ (aString isMemberOf: Symbol)
		ifTrue: [aString]	"putting old symbol in new table"
		ifFalse: [(Symbol new: aString size) string: aString]. "create a new one"

	lastNilIndex isNil
		ifTrue:[mainTable at: index put: (table copyWith: sym)]
		ifFalse:[table at: lastNilIndex put: sym].
	^sym
