copyLoop
	| prevWord thisWord skewWord halftoneWord mergeWord hInc y unskew skewMask notSkewMask mergeFnwith destWord |
	"This version of the inner loop assumes noSource = false."
	self inline: false.
	self var: #mergeFnwith declareC: 'int (*mergeFnwith)(int, int)'.
	mergeFnwith _ self cCoerce: (opTable at: combinationRule+1) to: 'int (*)(int, int)'.
	mergeFnwith.  "null ref for compiler"
	hInc _ hDir*4.  "Byte delta"
	"degenerate skew fixed for Sparc. 10/20/96 ikp"
	skew == -32
		ifTrue: [skew _ unskew _ skewMask _ 0]
		ifFalse: [skew < 0
			ifTrue:
				[unskew _ skew+32.
				skewMask _ AllOnes << (0-skew)]
			ifFalse:
				[skew = 0
					ifTrue:
						[unskew _ 0.
						skewMask _ AllOnes]
					ifFalse:
						[unskew _ skew-32.
						skewMask _ AllOnes >> skew]]].
	notSkewMask _ skewMask bitInvert32.
	noHalftone
		ifTrue: [halftoneWord _ AllOnes.  halftoneHeight _ 0]
		ifFalse: [halftoneWord _ self halftoneAt: 0].

	y _ dy.
	1 to: bbH do: "here is the vertical loop"
		[ :i |
		halftoneHeight > 1 ifTrue:  "Otherwise, its always the same"
			[halftoneWord _ self halftoneAt: y.
			y _ y + vDir].
		preload ifTrue:
			["load the 64-bit shifter"
			prevWord _ self srcLongAt: sourceIndex.
			sourceIndex _ sourceIndex + hInc]
			ifFalse:
			[prevWord _ 0].

	"Note: the horizontal loop has been expanded into three parts for speed:"

			"This first section requires masking of the destination store..."
			destMask _ mask1.
			thisWord _ self srcLongAt: sourceIndex.  "pick up next word"
			sourceIndex _ sourceIndex + hInc.
			skewWord _ ((prevWord bitAnd: notSkewMask) bitShift: unskew)
							bitOr:  "32-bit rotate"
						((thisWord bitAnd: skewMask) bitShift: skew).
			prevWord _ thisWord.
			destWord _ self dstLongAt: destIndex.
			mergeWord _ self mergeFn: (skewWord bitAnd: halftoneWord) with: destWord.
			destWord _ (destMask bitAnd: mergeWord) bitOr:
							(destWord bitAnd: destMask bitInvert32).
			self dstLongAt: destIndex put: destWord.
			destIndex _ destIndex + hInc.

		"This central horizontal loop requires no store masking"
		destMask _ AllOnes.
combinationRule = 3
ifTrue: [(skew = 0) & (halftoneWord = AllOnes)
		ifTrue:  
		["Very special inner loop for STORE mode with no skew -- just move words"
		2 to: nWords-1 do: 
			[ :word |  "Note loop starts with prevWord loaded (due to preload)"
			self dstLongAt: destIndex put: prevWord.
			destIndex _ destIndex + hInc.
			prevWord _ self srcLongAt: sourceIndex.
			sourceIndex _ sourceIndex + hInc]]
		ifFalse:
		["Special inner loop for STORE mode -- no need to call merge"
		2 to: nWords-1 do: 
			[ :word |
			thisWord _ self srcLongAt: sourceIndex.
			sourceIndex _ sourceIndex + hInc.
			skewWord _ ((prevWord bitAnd: notSkewMask) bitShift: unskew)
							bitOr:  "32-bit rotate"
						((thisWord bitAnd: skewMask) bitShift: skew).
			prevWord _ thisWord.
			self dstLongAt: destIndex put: (skewWord bitAnd: halftoneWord).
			destIndex _ destIndex + hInc]]
] ifFalse: [2 to: nWords-1 do: "Normal inner loop does merge:"
			[ :word |
			thisWord _ self srcLongAt: sourceIndex.  "pick up next word"
			sourceIndex _ sourceIndex + hInc.
			skewWord _ ((prevWord bitAnd: notSkewMask) bitShift: unskew)
							bitOr:  "32-bit rotate"
						((thisWord bitAnd: skewMask) bitShift: skew).
			prevWord _ thisWord.
			mergeWord _ self mergeFn: (skewWord bitAnd: halftoneWord)
							with: (self dstLongAt: destIndex).
			self dstLongAt: destIndex put: mergeWord.
			destIndex _ destIndex + hInc]
].

		"This last section, if used, requires masking of the destination store..."
		nWords > 1 ifTrue:
			[destMask _ mask2.
			thisWord _ self srcLongAt: sourceIndex.  "pick up next word"
			sourceIndex _ sourceIndex + hInc.
			skewWord _ ((prevWord bitAnd: notSkewMask) bitShift: unskew)
							bitOr:  "32-bit rotate"
						((thisWord bitAnd: skewMask) bitShift: skew).
			destWord _ self dstLongAt: destIndex.
			mergeWord _ self mergeFn: (skewWord bitAnd: halftoneWord) with: destWord.
			destWord _ (destMask bitAnd: mergeWord) bitOr:
							(destWord bitAnd: destMask bitInvert32).
			self dstLongAt: destIndex put: destWord.
			destIndex _ destIndex + hInc].

	sourceIndex _ sourceIndex + sourceDelta.
	destIndex _ destIndex + destDelta]