withArray: anArray 
	"Modifies self to contain the list of strings in anArray"
	| startOfLine endOfLine lineIndex aString |
	lines := Array new: 20.
	lastLine := 0.
	startOfLine := 1.
	endOfLine := 1.
	lineIndex := 0.
	anArray do: 
		[:item | 
		endOfLine := startOfLine + item size.		"this computation allows for a cr after each line..."
												"...but later we will adjust for no cr after last line"
		lineIndex := lineIndex + 1.
		self lineAt: lineIndex put:
			((TextLineInterval start: startOfLine stop: endOfLine
				internalSpaces: 0 paddingWidth: 0)
				lineHeight: textStyle lineGrid baseline: textStyle baseline).
		startOfLine := endOfLine + 1].
	endOfLine := endOfLine - 1.		"endOfLine is now the total size of the text"
	self trimLinesTo: lineIndex.
	aString := String new: endOfLine.
	anArray with: lines do: 
		[:item :interval | 
		aString
			replaceFrom: interval first
			to: interval last - 1
			with: item asString
			startingAt: 1.
		interval last <= endOfLine ifTrue: [aString at: interval last put: Character cr]].
	lineIndex > 0 ifTrue: [(lines at: lineIndex) stop: endOfLine].	"adjust for no cr after last line"
	self text: aString asText.
	anArray with: lines do: 
		[:item :interval |  item isText ifTrue:
			[text replaceFrom: interval first to: interval last - 1 with: item]].
	self updateCompositionHeight