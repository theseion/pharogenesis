endGentlyAfter: elapsed outOf: duration
	"Returns the proportion done for an animation using the EndGently style after elapsed seconds (out of duration seconds)."

	(elapsed >= duration) ifTrue: [ ^ 1.0 ].

	^ (self gently: (elapsed / duration) lowerBound: 0.001 upperBound: 0.01).
