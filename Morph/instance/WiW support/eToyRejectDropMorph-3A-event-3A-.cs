eToyRejectDropMorph: morphToDrop event: evt

	| tm am |

	tm _ TextMorph new 
		beAllFont: ((TextStyle named: Preferences standardEToysFont familyName) fontOfSize: 24);
		contents: 'GOT IT!'.
	(am _ AlignmentMorph new)
		color: Color yellow;
		layoutInset: 10;
		useRoundedCorners;
		vResizing: #shrinkWrap;
		hResizing: #shrinkWrap;
		addMorph: tm;
		fullBounds;
		position: (self bounds center - (am extent // 2));
		openInWorld: self world.
	SoundService default playSoundNamed: 'yum' ifAbsentReadFrom: 'yum.aif'.
	morphToDrop rejectDropMorphEvent: evt.		"send it back where it came from"
	am delete
