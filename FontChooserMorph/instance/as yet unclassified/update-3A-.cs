update: aSymbol
	super update: aSymbol.
	aSymbol == #selectedFontIndex
		ifTrue: [
			styleList ifNotNil:[styleList updateList].
			pointSizeList ifNotNil:[pointSizeList updateList].
			self updatePreview].
	aSymbol == #selectedFontStyleIndex
		ifTrue: [
			self updatePreview].
	aSymbol == #pointSize
		ifTrue: [
			pointSizeList ifNotNil:[pointSizeList selectionIndex: model selectedPointSizeIndex]. 
			self updatePreview].
	aSymbol == #pointSizeList
		ifTrue: [
			pointSizeList ifNotNil:[pointSizeList updateList].
			self updatePreview].