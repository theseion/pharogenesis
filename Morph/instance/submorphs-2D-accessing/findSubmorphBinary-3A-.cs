findSubmorphBinary: aBlock
	"Use binary search for finding a specific submorph of the receiver. Caller must be certain that the ordering holds for the submorphs."
	^submorphs findBinary: aBlock ifNone:[nil].