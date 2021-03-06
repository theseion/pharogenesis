openModal: aSystemWindow
	"Open the given window locking the receiver until it is dismissed.
	Answer the system window.
	Restore the original keyboard focus when closed."

	|area mySysWin keyboardFocus|
	keyboardFocus := self activeHand keyboardFocus.
	mySysWin := self isSystemWindow ifTrue: [self] ifFalse: [self ownerThatIsA: SystemWindow].
	mySysWin ifNil: [mySysWin := self].
	mySysWin modalLockTo: aSystemWindow.
	area := RealEstateAgent maximumUsableArea.
	self class environment at: #Flaps ifPresent: [:cl |
		RealEstateAgent reduceByFlaps: area].
	aSystemWindow extent: aSystemWindow initialExtent.
	aSystemWindow position = (0@0)
		ifTrue: [aSystemWindow
				position: self activeHand position - (aSystemWindow extent // 2)].
	aSystemWindow
		bounds: (aSystemWindow bounds translatedToBeWithin: area).
	[ToolBuilder default runModal: aSystemWindow openAsIs]
		ensure: [mySysWin modalUnlockFrom: aSystemWindow.
				self activeHand newKeyboardFocus: keyboardFocus].
	^aSystemWindow