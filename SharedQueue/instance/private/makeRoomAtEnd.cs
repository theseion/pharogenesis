makeRoomAtEnd
	| contentsSize |
	readPosition = 1
		ifTrue: [contentsArray := contentsArray , (Array new: 10)]
		ifFalse: 
			[contentsSize := writePosition - readPosition.
			"BLT direction ok for this. Lots faster!!!!!! SqR!! 4/10/2000 10:47"
			contentsArray
				replaceFrom: 1
				to: contentsSize
				with: contentsArray
				startingAt: readPosition.
			"nil out remainder --bf 10/25/2005"
			contentsArray
				from: contentsSize+1
				to: contentsArray size
				put: nil.
			readPosition := 1.
			writePosition := contentsSize + 1]