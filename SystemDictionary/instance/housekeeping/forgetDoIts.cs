forgetDoIts	"Smalltalk forgetDoIts"
	Smalltalk allBehaviorsDo: "get rid of old DoIt methods"
		[:cl | cl removeSelectorSimply: #DoIt; removeSelectorSimply: #DoItIn:]

