ifEmpty: emptyBlock ifNotEmptyDo: notEmptyBlock
	"Evaluate emptyBlock if I'm empty, notEmptyBlock otherwise"
	"Evaluate the notEmptyBlock with the receiver as its argument"

	^ self isEmpty ifTrue: emptyBlock ifFalse: [notEmptyBlock value: self]