blankGlyphForSeparators

	| space |
	space _ (self at: Character space charCode) copy.
	space contours: #().
	Character separators do: [:s | 
		glyphTable at: s charCode +1 put: space.
	].
