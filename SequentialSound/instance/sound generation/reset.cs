reset

	super reset.
	sounds do: [:snd | snd reset].
	sounds size > 0 ifTrue: [currentIndex _ 1].
