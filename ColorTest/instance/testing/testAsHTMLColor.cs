testAsHTMLColor

	| table aColorString |
	table := #('0' '1' '2' '3' '4' '5' '6' '7' '8' '9' 'A' 'B' 'C' 'D' 'E' 'F').

	table do: [ :each |
		aColorString := '#', each, each, '0000'.
		self assert: ((Color fromString: aColorString) asHTMLColor sameAs: aColorString)].

	table do: [ :each |
		aColorString := '#', '00', each, each, '00'.
		self assert: ((Color fromString: aColorString) asHTMLColor sameAs: aColorString)].

	table do: [ :each |
		aColorString := '#', '0000', each, each.
		self assert: ((Color fromString: aColorString) asHTMLColor sameAs: aColorString)].

	table do: [ :each |
		aColorString := '#', each, each, each, each, each, each.
		self assert: ((Color fromString: aColorString) asHTMLColor sameAs: aColorString)].