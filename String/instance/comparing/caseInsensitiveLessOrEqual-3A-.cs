caseInsensitiveLessOrEqual: aString 
	"Answer whether the receiver sorts before or equal to aString.
	The collation order is case insensitive."

	^ (self compare: self with: aString collated: CaseInsensitiveOrder) <= 2