shuffledBy: aRandom
	| copy | 
	copy := self shallowCopy.
	copy size to: 1 by: -1 do: 
		[:i | copy swap: i with: ((1 to: i) atRandom: aRandom)].
	^ copy