handleListenEvent: evt
	"Make sure we lock our contents after DnD has finished"
	evt isMouse ifFalse:[^self].
	evt hand hasSubmorphs ifTrue:[^self]. "still dragging"
	self == TopWindow ifFalse:[self lockInactivePortions].
	evt hand removeMouseListener: self.