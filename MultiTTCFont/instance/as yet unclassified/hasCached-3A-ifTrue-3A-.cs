hasCached: char ifTrue: twoArgBlock

	| value elem |
	value _ char asciiValue.

	self cache size to: 1 by: -1 do: [:i |
		elem _ self cache at: i.
		(elem first = value and: [elem second = foregroundColor]) ifTrue: [
			^ twoArgBlock value: elem third value: i.
		].
	].
	^ false.
