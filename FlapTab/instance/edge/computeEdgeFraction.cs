computeEdgeFraction
	"Compute and remember the edge fraction"

	| aBox aFraction |
	self isCurrentlySolid ifTrue: [^ edgeFraction ifNil: [self edgeFraction: 0.5]].

	aBox _ ((owner ifNil: [ActiveWorld]) bounds) insetBy: (self extent // 2).
	aFraction _ self
		ifVertical: 
			[(self center y - aBox top) / (aBox height max: 1)]
		ifHorizontal:
			[(self center x - aBox left) / (aBox width max: 1)].
	^ self edgeFraction: aFraction