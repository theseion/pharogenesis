installExternalFontFileName: fileName inDir: dir encoding: encoding encodingName: aString textStyleName: styleName

	| array arrayFour oldStyle arrayOfFS fs fonts newFonts |
	array := (ReferenceStream on: (dir readOnlyFileNamed: fileName)) next.

	arrayFour := Array new: 4 withAll: array last.
	arrayFour replaceFrom: 1 to: array size with: array startingAt: 1.
	TextConstants at: aString asSymbol put: arrayFour.

	oldStyle := TextConstants at: styleName asSymbol.
	arrayOfFS := oldStyle fontArray.
	arrayOfFS := (1 to: 4) collect: [:i |
		fs := arrayOfFS at: i.
		fonts := fs fontArray.
		encoding + 1 > fonts size ifTrue: [
			newFonts := Array new: encoding + 1.
			newFonts replaceFrom: 1 to: fonts size with: fonts startingAt: 1.
			newFonts at: encoding + 1 put: (arrayFour at: i).
			fs initializeWithFontArray: newFonts.
		] ifFalse: [
			fonts at: encoding + 1 put: (arrayFour at: i).
		].
		fs.
	].

	TextConstants at: styleName asSymbol put: (TextStyle fontArray: arrayOfFS).
	oldStyle becomeForward: (TextConstants at: styleName asSymbol).

