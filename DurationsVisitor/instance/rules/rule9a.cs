rule9a
	"Rule 9a: Postvocalic context of vowels."

	| events current next nextnext |
	phrase lastSyllable == syllable ifTrue: [^ self].
	events _ syllable events.
	1 to: events size do: [ :i |
		current _ events at: i.
		next _ i + 1 <= events size ifTrue: [(events at: i + 1) phoneme].
		nextnext _ i + 2 <= events size ifTrue: [(events at: i + 2) phoneme].
		current stretch: (self rule9a: current phoneme next: next nextnext: nextnext)]