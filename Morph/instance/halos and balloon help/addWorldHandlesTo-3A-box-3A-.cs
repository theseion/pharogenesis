addWorldHandlesTo: aHaloMorph box: box
	aHaloMorph haloBox: box.
	Preferences haloSpecificationsForWorld do:
		[:aSpec | 
			aHaloMorph perform: aSpec addHandleSelector with: aSpec].
	aHaloMorph innerTarget addOptionalHandlesTo: aHaloMorph box: box