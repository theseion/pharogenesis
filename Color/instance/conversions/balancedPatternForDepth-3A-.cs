balancedPatternForDepth: depth 
	"A generalization of bitPatternForDepth: as it exists.  Generates a 2x2 stipple of color.
	The topLeft and bottomRight pixel are closest approx to this color"
	| pv1 pv2 mask1 mask2 pv3 c |
	(depth == cachedDepth and: [ cachedBitPattern size = 2 ]) ifTrue: [ ^ cachedBitPattern ].
	(depth 
		between: 4
		and: 16) ifFalse: [ ^ self bitPatternForDepth: depth ].
	cachedDepth := depth.
	pv1 := self pixelValueForDepth: depth.
	"
	Subtract error due to pv1 to get pv2.
	pv2 _ (self - (err1 _ (Color colorFromPixelValue: pv1 depth: depth) - self))
						pixelValueForDepth: depth.
	Subtract error due to 2 pv1's and pv2 to get pv3.
	pv3 _ (self - err1 - err1 - ((Color colorFromPixelValue: pv2 depth: depth) - self))
						pixelValueForDepth: depth.
"
	"Above two statements computed faster by the following..."
	pv2 := (c := self - ((Color 
			colorFromPixelValue: pv1
			depth: depth) - self)) pixelValueForDepth: depth.
	pv3 := c + (c - (Color 
				colorFromPixelValue: pv2
				depth: depth)) pixelValueForDepth: depth.

	"Return to a 2-word bitmap that encodes a 2x2 stipple of the given pixelValues."
	mask1 := #(
		#-
		#-
		#-
		16843009
		#-
		#-
		#-
		65537
		#-
		#-
		#-
		#-
		#-
		#-
		#-
		1
	) at: depth.	"replicates every other 4 bits"	"replicates every other 8 bits"	"replicates every other 16 bits"
	mask2 := #(
		#-
		#-
		#-
		269488144
		#-
		#-
		#-
		16777472
		#-
		#-
		#-
		#-
		#-
		#-
		#-
		65536
	) at: depth.	"replicates the other 4 bits"	"replicates the other 8 bits"	"replicates the other 16 bits"
	^ cachedBitPattern := Bitmap 
		with: mask1 * pv1 + (mask2 * pv2)
		with: mask1 * pv3 + (mask2 * pv1)