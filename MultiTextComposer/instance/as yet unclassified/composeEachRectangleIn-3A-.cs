composeEachRectangleIn: rectangles

	| myLine lastChar |

	1 to: rectangles size do: [:i | 
		currCharIndex <= theText size ifFalse: [^false].
		myLine := scanner 
			composeFrom: currCharIndex 
			inRectangle: (rectangles at: i)				
			firstLine: isFirstLine 
			leftSide: i=1 
			rightSide: i=rectangles size.
		lines addLast: myLine.
		presentationLines addLast: scanner getPresentationLine.
		presentation ifNil: [presentation := scanner getPresentation]
			ifNotNil: [presentation := presentation, scanner getPresentation].
		actualHeight := actualHeight max: myLine lineHeight.  "includes font changes"
		currCharIndex := myLine last + 1.
		lastChar := theText at: myLine last.
		lastChar = Character cr ifTrue: [^#cr].
		wantsColumnBreaks ifTrue: [
			lastChar = TextComposer characterForColumnBreak ifTrue: [^#columnBreak].
		].
	].
	^false