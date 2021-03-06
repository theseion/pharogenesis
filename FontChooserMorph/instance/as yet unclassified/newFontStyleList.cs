newFontStyleList 
	| answer fon max |
	answer := PluggableListMorph
		on: self model
		list: #fontStyleListStrings
		selected: #selectedFontStyleIndex
		changeSelected: #selectedFontStyleIndex:.
	fon := answer font.
	max := fon widthOfStringOrText: 'Condensed Extra Bold Oblique' "long, but not the longest".
	model fontStyleList do:[:fontFamilyMember |
		max := max max: (fon widthOfStringOrText: fontFamilyMember styleName)].	
	answer	
			color: Color white;
			borderInset;
			vResizing: #spaceFill;
			hResizing: #spaceFill;
			"hResizing: #rigid;"
			width: max + answer scrollBarThickness + (fon widthOfStringOrText: '  ');
			yourself.
	^answer