variablesAssignedTo
	"Answer a collection of variables assigned to by this method."

	| refs |
	refs _ Set new.
	parseTree nodesDo: [ :node |
		node isAssignment ifTrue: [ refs add: node variable name ].
	].
	^ refs