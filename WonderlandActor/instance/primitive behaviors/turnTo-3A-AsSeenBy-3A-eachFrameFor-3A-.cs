turnTo: aVector AsSeenBy: reference eachFrameFor: aLifetime
	"Turns the object to the specified orientation in its parent's coordinate system each frame for the specified lifetime."

	"Check our arguments to make sure they're valid"
	[ WonderlandVerifier VerifyTarget: aVector ]
		ifError: [ :msg :rcvr |
			myWonderland reportErrorToUser: 'Squeak could not determine where to turn ' , myName , ' to because ', msg.
			^ nil ].

	[ WonderlandVerifier VerifyReferenceFrame: reference ]
		ifError: [ :msg :rcvr |
			myWonderland reportErrorToUser: 'Squeak can not turn ' , myName , ' relative to ' , (reference asString) , ' because ', msg.
			^ nil ].

	[ WonderlandVerifier VerifyNonNegativeNumber: aLifetime ]
		ifError: [ :msg :rcvr |
			myWonderland reportErrorToUser: 'Squeak could not determine how long it should turn ' , myName , ' because ', msg.
			^ nil ].

	myWonderland getUndoStack push: (UndoPOVChange for: self from: (self getPointOfView)).

	^ self doEachFrame: [ self turnToRightNow: aVector asSeenBy: reference undoable: false ]
			for: aLifetime.

