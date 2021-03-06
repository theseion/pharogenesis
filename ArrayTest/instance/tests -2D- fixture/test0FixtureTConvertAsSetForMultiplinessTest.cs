test0FixtureTConvertAsSetForMultiplinessTest
	"a collection ofFloat with equal elements:"
	| res |
	self 
		shouldnt: [ self withEqualElements ]
		raise: Error.
	self 
		shouldnt: 
			[ self withEqualElements do: [ :each | self assert: each class = Float ] ]
		raise: Error.
	res := true.
	self withEqualElements 
		detect: [ :each | (self withEqualElements occurrencesOf: each) > 1 ]
		ifNone: [ res := false ].
	self assert: res = true.

	"a collection of Float without equal elements:"
	self 
		shouldnt: [ self elementsCopyNonIdenticalWithoutEqualElements ]
		raise: Error.
	self 
		shouldnt: 
			[ self elementsCopyNonIdenticalWithoutEqualElements do: [ :each | self assert: each class = Float ] ]
		raise: Error.
	res := true.
	self elementsCopyNonIdenticalWithoutEqualElements 
		detect: 
			[ :each | 
			(self elementsCopyNonIdenticalWithoutEqualElements occurrencesOf: each) > 1 ]
		ifNone: [ res := false ].
	self assert: res = false