addHandlesTo: aHaloMorph box: box
	| onlyThese |
	aHaloMorph haloBox: box.
	onlyThese := #(addDismissHandle: addMenuHandle: addGrabHandle: addDragHandle: addDupHandle: addHelpHandle: addGrowHandle: addFontSizeHandle: addFontStyleHandle: addFontEmphHandle: addRecolorHandle:).
	Preferences haloSpecifications do:
		[:aSpec | (onlyThese includes: aSpec addHandleSelector) ifTrue:
				[aHaloMorph perform: aSpec addHandleSelector with: aSpec]].
	aHaloMorph innerTarget addOptionalHandlesTo: aHaloMorph box: box