readStereoChannelDataFrom: s
	"Read stereophonic channel data from the given stream. Each frame contains two samples."

	| left right w |
	left _ channelData at: 1.
	right _ channelData at: 2.
	bitsPerSample = 8
		ifTrue: [
			1 to: frameCount do: [:i |
				w _ s next.
				w > 127 ifTrue: [w _ w - 256].
				left at: i put: (w bitShift: 8).
				w _ s next.
				w > 127 ifTrue: [w _ w - 256].
				right at: i put: (w bitShift: 8)]]
		ifFalse: [
			1 to: frameCount do: [:i |
				w _ (s next bitShift: 8) + s next.
				w > 32767 ifTrue: [w _ w - 65536].
				left at: i put: w.
				w _ (s next bitShift: 8) + s next.
				w > 32767 ifTrue: [w _ w - 65536].
				right at: i put: w]].
