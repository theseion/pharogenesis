doControl

	super doControl.
	1 to: activeSounds size do: [:i | (activeSounds at: i) first doControl].
	ticksSinceStart _ ticksSinceStart + ticksClockIncr.
	self processAllAtTick: ticksSinceStart asInteger.
