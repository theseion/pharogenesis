customizeForEToyUsers: aBoolean
	"If aBoolean is true, set things up for etoy users.  If it's false, unset some of those things.  Some things are set when switching into etoy mode but not reversed when switching out of etoy mode"
 
	#(	
		(balloonHelpEnabled			yes		dontReverse)
		(debugHaloHandle			no		reverse)
		(modalColorPickers			yes		dontReverse)
		(oliveHandleForScriptedObjects	no	dontReverse)
		(uniqueNamesInHalos		yes		reverse)
		(useUndo					yes		dontReverse)
		(infiniteUndo				no		dontReverse)
		(warnIfNoChangesFile		no		reverse)
		(warnIfNoSourcesFile		no		reverse)) do:
			[:trip |
				(aBoolean or: [trip third == #reverse]) ifTrue:
					[Preferences enableOrDisable: trip first asPer:
						((trip second == #yes) & aBoolean) | ((trip second == #no) & aBoolean not)]]