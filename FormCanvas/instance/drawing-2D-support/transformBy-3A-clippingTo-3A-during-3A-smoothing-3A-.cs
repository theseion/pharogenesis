transformBy: aDisplayTransform clippingTo: aClipRect during: aBlock	 smoothing: cellSize

	"Note: This method has been originally copied from TransformationMorph."
	| innerRect patchRect sourceQuad warp start subCanvas |
	(aDisplayTransform isPureTranslation) ifTrue:[
		^aBlock value: (self copyOffset: aDisplayTransform offset negated truncated
							clipRect: aClipRect)
	].
	"Prepare an appropriate warp from patch to innerRect"
	innerRect _ aClipRect.
	patchRect _ aDisplayTransform globalBoundsToLocal: innerRect.
	sourceQuad _ (aDisplayTransform sourceQuadFor: innerRect)
					collect: [:p | p - patchRect topLeft].
	warp _ self warpFrom: sourceQuad toRect: innerRect.
	warp cellSize: cellSize.

	"Render the submorphs visible in the clipping rectangle, as patchForm"
	start _ (self depth = 1 and: [self isShadowDrawing not])
		"If this is true B&W, then we need a first pass for erasure."
		ifTrue: [1] ifFalse: [2].
	start to: 2 do:
		[:i | "If i=1 we first make a shadow and erase it for opaque whites in B&W"
		subCanvas _ self class extent: patchRect extent depth: self depth.
		i=1	ifTrue: [subCanvas shadowColor: Color black.
					warp combinationRule: Form erase]
			ifFalse: [self isShadowDrawing ifTrue:
					[subCanvas shadowColor: self shadowColor].
					warp combinationRule: Form paint].
		subCanvas translateBy: patchRect topLeft negated
			during:[:offsetCanvas| aBlock value: offsetCanvas].
		warp sourceForm: subCanvas form; warpBits.
		warp sourceForm: nil.  subCanvas _ nil "release space for next loop"]
