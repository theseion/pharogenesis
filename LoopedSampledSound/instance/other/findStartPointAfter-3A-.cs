findStartPointAfter: index
	"Answer the index of the last zero crossing sample before the given index."

	| i |
	i _ index min: lastSample.

	"scan backwards to the last zero-crossing"
	(leftSamples at: i) > 0
		ifTrue: [
			[i > 1 and: [(leftSamples at: i) > 0]] whileTrue: [i _ i - 1]]
		ifFalse: [
			[i > 1 and: [(leftSamples at: i) < 0]] whileTrue: [i _ i - 1]].
	^ i
