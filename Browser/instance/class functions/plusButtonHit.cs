plusButtonHit
	"Cycle among definition, comment, and hierachy"

	editSelection == #editComment
		ifTrue: [self hierarchy. ^ self].
	editSelection == #hierarchy
		ifTrue: [self editSelection: #editClass.
			classListIndex = 0 ifTrue: [^ self].
			self okToChange ifFalse: [^ self].
			self changed: #editComment.
			self contentsChanged.
			^ self].
	self editComment