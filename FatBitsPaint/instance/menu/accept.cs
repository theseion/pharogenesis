accept
	| f |
	f _ self unmagnifiedForm.
	f boundingBox = formToEdit boundingBox
		ifFalse: [^ self error: 'implementation error; form dimensions should match'].
	f displayOn: formToEdit.  "modify formToEdit in place"