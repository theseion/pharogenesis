residesInPartsBin
	"Answer true if the receiver is, or has some ancestor owner who is, a parts bin"

	self isWorldMorph
		ifTrue: [^ self isPartsBin]
		ifFalse: [^ self isPartsBin or: [super residesInPartsBin]]