condensedStereoSound
	"Decompose my buffers into left and right channels and return a mixed sound consisting of the those two channels. This may be take a while, since the data must be copied into new buffers."

	| sz leftBuf rightBuf leftI rightI left |
	sz _ recordedBuffers inject: 0 into: [:tot :buff | tot + buff size].
	leftBuf _ SoundBuffer newMonoSampleCount: (sz + 1) // 2.
	rightBuf _ SoundBuffer newMonoSampleCount: (sz + 1) // 2.
	leftI _ rightI _ 1.
	left _ true.
	recordedBuffers do: [:b |
		1 to: b size do: [:j |
			left
				ifTrue: [leftBuf at: leftI put: (b at: j). leftI _ leftI + 1. left _ false]
				ifFalse: [rightBuf at: rightI put: (b at: j). rightI _ rightI + 1. left _ true]]].
	^ MixedSound new
		add: (SampledSound new setSamples: leftBuf samplingRate: samplingRate) pan: 0.0;
		add: (SampledSound new setSamples: rightBuf samplingRate: samplingRate) pan: 1.0
