decimalDigitLength
	"Answer the number of digits printed out in base 10.
	Note that this only works for positive SmallIntegers."
	
	^ self < 10000
		ifTrue: [self < 100
				ifTrue: [self < 10
						ifTrue: [1]
						ifFalse: [2]]
				ifFalse: [self < 1000
						ifTrue: [3]
						ifFalse: [4]]]
		ifFalse: [self < 1000000
				ifTrue: [self < 100000
						ifTrue: [5]
						ifFalse: [6]]
				ifFalse: [self < 100000000
						ifTrue: [self < 10000000
								ifTrue: [7]
								ifFalse: [8]]
						ifFalse: [self < 1000000000
								ifTrue: [9]
								ifFalse: [10]]]]