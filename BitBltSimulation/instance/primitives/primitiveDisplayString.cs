primitiveDisplayString
	| kernDelta xTable glyphMap stopIndex startIndex sourceString bbObj maxGlyph ascii glyphIndex sourcePtr left |
	self export: true.
	self var: #sourcePtr type: 'unsigned char *'.
	interpreterProxy methodArgumentCount = 6 
		ifFalse:[^interpreterProxy primitiveFail].
	kernDelta _ interpreterProxy stackIntegerValue: 0.
	xTable _ interpreterProxy stackObjectValue: 1.
	glyphMap _ interpreterProxy stackObjectValue: 2.
	((interpreterProxy fetchClassOf: xTable) = interpreterProxy classArray and:[
		(interpreterProxy fetchClassOf: glyphMap) = interpreterProxy classArray])
			ifFalse:[^interpreterProxy primitiveFail].
	(interpreterProxy slotSizeOf: glyphMap) = 256 ifFalse:[^interpreterProxy primitiveFail].
	interpreterProxy failed ifTrue:[^nil].
	maxGlyph _ (interpreterProxy slotSizeOf: xTable) - 2.

	stopIndex _ interpreterProxy stackIntegerValue: 3.
	startIndex _ interpreterProxy stackIntegerValue: 4.
	sourceString _ interpreterProxy stackObjectValue: 5.
	(interpreterProxy isBytes: sourceString) ifFalse:[^interpreterProxy primitiveFail].
	(startIndex > 0 and:[stopIndex > 0 and:[
		stopIndex <= (interpreterProxy byteSizeOf: sourceString)]])
			ifFalse:[^interpreterProxy primitiveFail].

	bbObj _ interpreterProxy stackObjectValue: 6.
	(self loadBitBltFrom: bbObj) ifFalse:[^interpreterProxy primitiveFail].
	left _ destX.
	sourcePtr _ interpreterProxy firstIndexableField: sourceString.
	startIndex to: stopIndex do:[:charIndex|
		ascii _ sourcePtr at: charIndex-1.
		glyphIndex _ interpreterProxy fetchInteger: ascii ofObject: glyphMap.
		(glyphIndex < 0 or:[glyphIndex > maxGlyph]) 
			ifTrue:[^interpreterProxy primitiveFail].
		sourceX _ interpreterProxy fetchInteger: glyphIndex ofObject: xTable.
		width _ (interpreterProxy fetchInteger: glyphIndex+1 ofObject: xTable) - sourceX.
		interpreterProxy failed ifTrue:[^nil].
		self clipRange.	"Must clip here"
		(bbW > 0 and:[bbH > 0]) ifTrue: [self copyBits].
		interpreterProxy failed ifTrue:[^nil].
		destX _ destX + width + kernDelta.
	].
	affectedL _ left.
	self showDisplayBits.
	interpreterProxy pop: 6. "pop args, return rcvr"