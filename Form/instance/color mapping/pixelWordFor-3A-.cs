pixelWordFor: aColor
	"Return the pixel word for representing the given color on the receiver"
	| basicPattern |
	self hasNonStandardPalette 
		ifFalse:[^aColor pixelWordForDepth: self depth].
	basicPattern _ self pixelValueFor: aColor.
	self depth = 32 
		ifTrue:[^basicPattern]
		ifFalse:[^aColor pixelWordFor: self depth filledWith: basicPattern]