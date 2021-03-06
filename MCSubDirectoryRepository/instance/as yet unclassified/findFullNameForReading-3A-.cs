findFullNameForReading: aBaseName
	"Answer the latest version of aBaseName"
	| possible |
	possible := SortedCollection sortBlock: [ :a :b | b first modificationTime < a first modificationTime ].
	self allDirectories
		do: [:dir | dir entries
				do: [:ent | ent isDirectory
						ifFalse: [
							(ent name = aBaseName) ifTrue: [ possible add: {ent. dir fullNameFor: ent name}]]]].
	^(possible at: 1 ifAbsent: [ ^nil ]) second
