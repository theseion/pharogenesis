subCanvas:patchRect
	subCanvas ifNil:
		[ subCanvas _ PostscriptCanvas new reset setOrigin:patchRect topLeft clipRect:(-10000@-10000 extent:20000@20000)].
	^subCanvas.

