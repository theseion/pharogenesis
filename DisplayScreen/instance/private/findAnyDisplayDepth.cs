findAnyDisplayDepth
	"Return any display depth that is supported on this system."
	^self findAnyDisplayDepthIfNone:[
		"Ugh .... now this is a biggie - a system that does not support
		any of the Squeak display depths at all."
		Smalltalk
			logError:'Fatal error: This system has no support for any display depth at all.'
			inContext: thisContext
			to: 'SqueakDebug.log'.
		Smalltalk quitPrimitive. "There is no way to continue from here"
	].