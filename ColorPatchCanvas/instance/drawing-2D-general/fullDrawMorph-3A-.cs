fullDrawMorph: aMorph
	(foundMorph and:[doStop]) ifTrue:[^self]. "Found it and should stop"
	aMorph == stopMorph ifTrue:[
		"Never draw the stopMorph"
		foundMorph _ true.
		^self].
	^super fullDrawMorph: aMorph.