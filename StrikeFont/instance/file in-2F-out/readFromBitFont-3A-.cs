readFromBitFont: fileName
	"This builds a StrikeFont instance by reading the data file format
	produced by BitFont, a widely available font conversion utility
	written by Peter DiCamillo at Brown University"
	"StrikeFont new readFromBitFont: 'Palatino10.BF' "
	| f lastAscii charLine width ascii charForm line missingForm tempGlyphs iRect p rectLine left tokens right |
	f _ FileStream readOnlyFileNamed: fileName.
	self readBFHeaderFrom: f.

	"NOTE: if font has been scaled (and in any case),
	the REAL bitmap dimensions come after the header."
	self restOfLine: 'Extent information for entire font' from: f.
	"Parse the following line (including mispelling!)"
	"Image rectange: left = -2, right = 8, bottom = -2, top = 7"
	tokens _ (f upTo: Character cr)  findTokens: ' '.
	iRect _ Rectangle left: (tokens at: 5) asNumber right: (tokens at: 8) asNumber
				top: (tokens at: 14) asNumber bottom: (tokens at: 11) asNumber.
	ascent _ iRect top.
	descent _ iRect bottom negated.
	
	tempGlyphs _ Form extent: (maxWidth*257) @ self height.
	xTable _ (Array new: 258) atAllPut: 0.
	xTable at: 1 put: 0.

	"Read character forms and blt into tempGlyphs"
	lastAscii _ -1.
	[charLine _ self restOfLine: 'Character: ' from: f.
	charLine == nil ifFalse:
		[p _ f position.
		rectLine _ f upTo: Character cr.
		(rectLine beginsWith: 'Image rectange: left = ')
			ifTrue: [tokens _ rectLine findTokens: ' '.
					left _ (tokens at: 5) asNumber. right _ (tokens at: 8) asNumber]
			ifFalse: [left _ right _ 0. f position: p].
		width_ (self restOfLine: 'Width (final pen position) = ' from: f) asNumber - left
					max: (right-left+1).
		(charLine beginsWith: 'Missing character') ifTrue: [ascii _ 256].
		('x''*' match: charLine) ifTrue:
			[ascii _ Number readFrom: (charLine copyFrom: 3 to: 4) asUppercase base: 16].
		charForm _ Form extent: width@self height.
		('*[all blank]' match: charLine) ifFalse:
			[self restOfLine: '  +' from: f.
			1 to: self height do:
				[:y | line _ f upTo: Character cr.
				4 to: (width + 3 min: line size + iRect left - left) do:
					[:x | (line at: x - iRect left + left) = $*
						ifTrue: [charForm pixelValueAt: (x-4)@(y-1) put: 1]]]]].
	charLine == nil]
		whileFalse:
			[self displayChar: ascii form: charForm.
			ascii = 256
				ifTrue: [missingForm _ charForm deepCopy]
				ifFalse:
				[minAscii _ minAscii min: ascii.
				maxAscii _ maxAscii max: ascii.
				lastAscii+1 to: ascii-1 do: [:a | xTable at: a+2 put: (xTable at: a+1)].
				tempGlyphs copy: ((xTable at: ascii+1)@0
										extent: charForm extent)
							from: 0@0 in: charForm rule: Form over.
				xTable at: ascii+2 put: (xTable at: ascii+1) + width.
				lastAscii _ ascii]].
	f close.
	lastAscii+1 to: maxAscii+1 do: [:a | xTable at: a+2 put: (xTable at: a+1)].
	missingForm == nil ifFalse:
		[tempGlyphs copy: missingForm boundingBox from: missingForm
				to: (xTable at: maxAscii+2)@0 rule: Form over.
		xTable at: maxAscii+3 put: (xTable at: maxAscii+2) + missingForm width].
	glyphs _ Form extent: (xTable at: maxAscii+3) @ self height.
	glyphs copy: glyphs boundingBox from: 0@0 in: tempGlyphs rule: Form over.
	xTable _ xTable copyFrom: 1 to: maxAscii+3.

	self setStopConditions