soundOfDuration: duration

	| sound |
	sound _ MixedSound new.
	sound add: (self noteOfDuration: duration)
		pan: (owner scorePlayer panForTrack: trackIndex)
		volume: owner scorePlayer overallVolume *
				(owner scorePlayer volumeForTrack: trackIndex).
	^ sound reset
