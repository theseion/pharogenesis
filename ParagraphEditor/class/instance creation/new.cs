new
	"Answer a new instance of me with a null Paragraph to be edited."

	| aParagraphEditor |
	aParagraphEditor := super new.
	aParagraphEditor changeParagraph: '' asParagraph.
	^aParagraphEditor