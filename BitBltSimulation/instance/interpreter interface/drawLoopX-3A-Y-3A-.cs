drawLoopX: xDelta Y: yDelta 
	"This is the primitive implementation of the line-drawing loop.
	See the comments in BitBlt>>drawLoopX:Y:"
	| dx1 dy1 px py P affL affR affT affB |
	xDelta > 0
		ifTrue: [dx1 _ 1]
		ifFalse: [xDelta = 0
				ifTrue: [dx1 _ 0]
				ifFalse: [dx1 _ -1]].
	yDelta > 0
		ifTrue: [dy1 _ 1]
		ifFalse: [yDelta = 0
				ifTrue: [dy1 _ 0]
				ifFalse: [dy1 _ -1]].
	px _ yDelta abs.
	py _ xDelta abs.
	affL _ affT _ 9999.  "init null rectangle"
	affR _ affB _ -9999.
	py > px
		ifTrue: 
			["more horizontal"
			P _ py // 2.
			1 to: py do: 
				[:i |
				destX _ destX + dx1.
				(P _ P - px) < 0 ifTrue: 
					[destY _ destY + dy1.
					P _ P + py].
				i < py ifTrue:
					[self copyBits.
					(affectedL < affectedR and: [affectedT < affectedB]) ifTrue:
						["Affected rectangle grows along the line"
						affL _ affL min: affectedL.
						affR _ affR max: affectedR.
						affT _ affT min: affectedT.
						affB _ affB max: affectedB.
						(affR - affL) * (affB - affT) > 4000 ifTrue:
							["If affected rectangle gets large, update it in chunks"
							affectedL _ affL.  affectedR _ affR.
							affectedT _ affT.  affectedB _ affB.
							interpreterProxy showDisplayBits.
							affL _ affT _ 9999.  "init null rectangle"
							affR _ affB _ -9999]].
					]]]
		ifFalse: 
			["more vertical"
			P _ px // 2.
			1 to: px do:
				[:i |
				destY _ destY + dy1.
				(P _ P - py) < 0 ifTrue: 
					[destX _ destX + dx1.
					P _ P + px].
				i < px ifTrue:
					[self copyBits.
					(affectedL < affectedR and: [affectedT < affectedB]) ifTrue:
						["Affected rectangle grows along the line"
						affL _ affL min: affectedL.
						affR _ affR max: affectedR.
						affT _ affT min: affectedT.
						affB _ affB max: affectedB.
						(affR - affL) * (affB - affT) > 4000 ifTrue:
							["If affected rectangle gets large, update it in chunks"
							affectedL _ affL.  affectedR _ affR.
							affectedT _ affT.  affectedB _ affB.
							interpreterProxy showDisplayBits.
							affL _ affT _ 9999.  "init null rectangle"
							affR _ affB _ -9999]].
					]]].

	"Remaining affected rect"
	affectedL _ affL.  affectedR _ affR.
	affectedT _ affT.  affectedB _ affB.

	"store destX, Y back"	
	interpreterProxy storeInteger: BBDestXIndex ofObject: bitBltOop withValue: destX.
	interpreterProxy storeInteger: BBDestYIndex ofObject: bitBltOop withValue: destY.