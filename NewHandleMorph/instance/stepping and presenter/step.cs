step
	| eventSource |

	eventSource := self sensorMode ifTrue: [
		Sensor
	] ifFalse: [
		hand lastEvent
	].
	eventSource anyButtonPressed
		ifTrue: [waitingForClickInside := false.
				self position: eventSource cursorPoint - (self extent // 2).
				pointBlock value: self center]
		ifFalse: [waitingForClickInside
					ifTrue: [(self containsPoint: eventSource cursorPoint)
								ifFalse: ["mouse wandered out before clicked"
										^ self delete]]
					ifFalse: [lastPointBlock value: self center.
							^ self delete]]