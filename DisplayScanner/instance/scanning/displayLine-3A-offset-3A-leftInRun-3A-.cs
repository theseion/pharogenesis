displayLine: textLine offset: offset leftInRun: leftInRun 
	"The call on the primitive (scanCharactersFrom:to:in:rightX:) will be interrupted according to an array of stop conditions passed to the scanner at which time the code to handle the stop condition is run and the call on the primitive continued until a stop condition returns true (which means the line has terminated).  leftInRun is the # of characters left to scan in the current run; when 0, it is time to call setStopConditions."
	| done stopCondition nowLeftInRun startIndex string lastPos |
	line := textLine.
	morphicOffset := offset.
	lineY := line top + offset y.
	lineHeight := line lineHeight.
	rightMargin := line rightMargin + offset x.
	lastIndex := line first.
	leftInRun <= 0 ifTrue: [ self setStopConditions ].
	leftMargin := (line leftMarginForAlignment: alignment) + offset x.
	destX := runX := leftMargin.
	fillBlt == nil ifFalse: 
		[ "Not right"
		fillBlt
			destX: line left
				destY: lineY
				width: line width left
				height: lineHeight;
			copyBits ].
	lastIndex := line first.
	leftInRun <= 0 
		ifTrue: [ nowLeftInRun := text runLengthFor: lastIndex ]
		ifFalse: [ nowLeftInRun := leftInRun ].
	destY := lineY + line baseline - font ascent.
	runStopIndex := lastIndex + (nowLeftInRun - 1) min: line last.
	spaceCount := 0.
	done := false.
	string := text string.
	[ done ] whileFalse: 
		[ startIndex := lastIndex.
		lastPos := destX @ destY.
		stopCondition := self 
			scanCharactersFrom: lastIndex
			to: runStopIndex
			in: string
			rightX: rightMargin
			stopConditions: stopConditions
			kern: kern.
		lastIndex >= startIndex ifTrue: 
			[ font 
				displayString: string
				on: bitBlt
				from: startIndex
				to: lastIndex
				at: lastPos
				kern: kern ].
		"see setStopConditions for stopping conditions for displaying."
		done := self perform: stopCondition.
		lastIndex > runStopIndex ifTrue: [ done := true ] ].
	^ runStopIndex - lastIndex	"Number of characters remaining in the current run"