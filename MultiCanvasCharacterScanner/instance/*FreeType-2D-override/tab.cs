tab

	pendingKernX := 0.
	destX := (alignment == Justified and: [self leadingTab not])
		ifTrue:		"imbedded tabs in justified text are weird"
			[destX + (textStyle tabWidth - (line justifiedTabDeltaFor: spaceCount)) max: destX]
		ifFalse: 
			[textStyle nextTabXFrom: destX
				leftMargin: leftMargin
				rightMargin: rightMargin].

	lastIndex := lastIndex + 1.
	^ false