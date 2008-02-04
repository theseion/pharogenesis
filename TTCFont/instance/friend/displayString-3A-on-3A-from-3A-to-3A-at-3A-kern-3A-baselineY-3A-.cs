displayString: aString on: aBitBlt from: startIndex to: stopIndex at: aPoint kern: kernDelta baselineY: baselineY

	| destPoint form glyphInfo destY |
	destPoint := aPoint.
	glyphInfo := Array new: 5.
	startIndex to: stopIndex do: [:charIndex |
		self glyphInfoOf: (aString at: charIndex) into: glyphInfo.
		form := glyphInfo first.
		((glyphInfo at:5) ~= aBitBlt lastFont) ifTrue: [
			(glyphInfo at:5) installOn: aBitBlt.
		].
		destY := baselineY - (glyphInfo at:4). 
		aBitBlt sourceForm: form.
		aBitBlt destX: destPoint x.
		aBitBlt destY: destY.
		aBitBlt sourceOrigin: (glyphInfo at:2) @ 0.
		aBitBlt width: glyphInfo third - glyphInfo second.
		aBitBlt height: form height.
		aBitBlt copyBits.
		destPoint := destPoint + (((glyphInfo at:3) - (glyphInfo at:2)) + kernDelta @ 0).
	].
	^ destPoint.
