printPaddedWith: aCharacter to: aNumber 
	"Answer the string containing the ASCII representation of the receiver 
	padded on the left with aCharacter to be at least on aNumber 
	integerPart characters and padded the right with aCharacter to be at 
	least anInteger fractionPart characters."
	| aStream digits fPadding fLen iPadding iLen curLen periodIndex |
	#Numeric.
	"2000/03/04  Harmon R. Added Date and Time support"
	aStream := WriteStream on: (String new: 10).
	self printOn: aStream.
	digits := aStream contents.
	periodIndex := digits indexOf: $..
	curLen := periodIndex - 1.
	iLen := aNumber integerPart.
	curLen < iLen
		ifTrue: [iPadding := (String new: (iLen - curLen) asInteger) atAllPut: aCharacter;
					 yourself]
		ifFalse: [iPadding := ''].
	curLen := digits size - periodIndex.
	fLen := (aNumber fractionPart * (aNumber asFloat exponent * 10)) asInteger.
	curLen < fLen
		ifTrue: [fPadding := (String new: fLen - curLen) atAllPut: aCharacter;
					 yourself]
		ifFalse: [fPadding := ''].
	^ iPadding , digits , fPadding