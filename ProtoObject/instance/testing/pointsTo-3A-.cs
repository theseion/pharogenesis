pointsTo: anObject
	"This method returns true if self contains a pointer to anObject,
		and returns false otherwise"
	<primitive: 132>
	1 to: self class instSize do:
		[:i | (self instVarAt: i) == anObject ifTrue: [^ true]].
	1 to: self basicSize do:
		[:i | (self basicAt: i) == anObject ifTrue: [^ true]].
	^ false