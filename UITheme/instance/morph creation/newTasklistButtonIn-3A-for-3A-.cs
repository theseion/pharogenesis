newTasklistButtonIn: aTasklist for: aTask
	"Answer a tasklist button morph for the given task."
	
	|lm lab button|
	lab := (self buttonLabelForText: aTask label).
	lm := self
		newRowIn: aTasklist
		for: {(aTask icon ifNil: [MenuIcons smallWindowIcon]) asMorph. lab}.
	button := self
		newButtonIn: aTasklist
		for: aTask morph
		getState: #isActive
		action: #buttonClickedForTasklist:
		arguments: {aTasklist}
		getEnabled: nil
		label: lm
		help: nil.
	button
		useSquareCorners;
		onColor: (self taskbarMinimizedButtonColorFor: button)
		offColor: (aTask isActive
				ifTrue: [self taskbarActiveButtonColorFor: button]
				ifFalse: [self taskbarButtonColorFor: button]);
		hResizing: #spaceFill.
	lab color: (self taskbarButtonLabelColorFor: button).
	button model: aTask.
	^button