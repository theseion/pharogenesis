removeStayUpBox
	| box ext |
	submorphs isEmpty ifTrue: [^self].
	(submorphs first isAlignmentMorph) ifFalse: [^self].
	box := submorphs first submorphs last.
	ext := box extent.
	(box isKindOf: IconicButton) 
		ifTrue: 
			[box
				labelGraphic: (Form extent: ext depth: 8);
				shedSelvedge;
				borderWidth: 0;
				lock].
		box extent: ext.