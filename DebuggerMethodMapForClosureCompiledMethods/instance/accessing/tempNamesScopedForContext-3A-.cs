tempNamesScopedForContext: aContext
	"Answer an Array of all the temp names in scope in aContext starting with
	 the home's first local (the first argument or first temporary if no arguments)."
	^((self
		privateTempRefsForContext: aContext
		startpcsToBlockExtents: aContext method startpcsToBlockExtents) reject: [:pair | self privateIsOuter: pair] )
		collect:
			[:pair| pair first]