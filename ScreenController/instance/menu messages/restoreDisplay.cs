restoreDisplay 
	"Clear the screen to gray and then redisplay all the scheduled views."

	Smalltalk isMorphic ifTrue: [^ World restoreMorphicDisplay].

	Display extent = DisplayScreen actualScreenSize
		ifFalse:
			[DisplayScreen startUp.
			ScheduledControllers unCacheWindows].
	ScheduledControllers restore