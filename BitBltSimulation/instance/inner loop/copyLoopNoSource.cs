copyLoopNoSource
	| halftoneWord mergeWord mergeFnwith destWord |
	"Faster copyLoop when source not used.  hDir and vDir are both
	positive, and perload and skew are unused"

	self inline: false.
	self var: #mergeFnwith declareC: 'int (*mergeFnwith)(int, int)'.
	mergeFnwith _ self cCoerce: (opTable at: combinationRule+1) to: 'int (*)(int, int)'.
	mergeFnwith.  "null ref for compiler"

	1 to: bbH do: "here is the vertical loop"
		[ :i |
		noHalftone
			ifTrue: [halftoneWord _ AllOnes]
			ifFalse: [halftoneWord _ self halftoneAt: dy+i-1].

	"Note: the horizontal loop has been expanded into three parts for speed:"

			"This first section requires masking of the destination store..."
			destMask _ mask1.
			destWord _ self dstLongAt: destIndex.
			mergeWord _ self mergeFn: halftoneWord
							with: destWord.
			destWord _ (destMask bitAnd: mergeWord) bitOr: 
							(destWord bitAnd: destMask bitInvert32).
			self dstLongAt: destIndex put: destWord.
			destIndex _ destIndex + 4.

		"This central horizontal loop requires no store masking"
			destMask _ AllOnes.
			combinationRule = 3 ifTrue: ["Special inner loop for STORE"
				destWord _ halftoneWord.
				2 to: nWords-1 do:[ :word |
					self dstLongAt: destIndex put: destWord.
					destIndex _ destIndex + 4].
			] ifFalse:[ "Normal inner loop does merge"
				2 to: nWords-1 do:[ :word | "Normal inner loop does merge"
					destWord _ self dstLongAt: destIndex.
					mergeWord _ self mergeFn: halftoneWord with: destWord.
					self dstLongAt: destIndex put: mergeWord.
					destIndex _ destIndex + 4].
			].

		"This last section, if used, requires masking of the destination store..."
		nWords > 1 ifTrue:
			[destMask _ mask2.
			destWord _ self dstLongAt: destIndex.
			mergeWord _ self mergeFn: halftoneWord with: destWord.
			destWord _ (destMask bitAnd: mergeWord) bitOr:
							(destWord bitAnd: destMask bitInvert32).
			self dstLongAt: destIndex put: destWord.
			destIndex _ destIndex + 4].

	destIndex _ destIndex + destDelta]