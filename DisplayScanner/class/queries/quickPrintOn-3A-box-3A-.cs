quickPrintOn: aForm box: aRectangle
	"Create an instance to print on the given form in the given rectangle."

	^(super new) quickPrintOn: aForm box: aRectangle font: self defaultFont color: Color black