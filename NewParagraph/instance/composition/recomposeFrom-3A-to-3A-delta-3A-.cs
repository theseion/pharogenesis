recomposeFrom: start to: stop delta: delta
	"Recompose this paragraph.  The altered portion is between start and stop.
	Recomposition may continue to the end of the text, due to a ripple effect.
	Delta is the amount by which the current text is longer than it was
	when its current lines were composed."
	| startLine newLines |
	"Have to recompose line above in case a word-break was affected."
	startLine _ (self lineIndexForCharacter: start) - 1 max: 1.
	[startLine > 1 and: [(lines at: startLine-1) top = (lines at: startLine) top]]
		whileTrue: [startLine _ startLine - 1].  "Find leftmost of line pieces"
	newLines _ OrderedCollection new: lines size + 1.
	1 to: startLine-1 do: [:i | newLines addLast: (lines at: i)].
	self composeLinesFrom: (lines at: startLine) first to: stop delta: delta
			into: newLines priorLines: lines
			atY: (lines at: startLine) top