doWhileFalse: conditionBlock
	"Evaluate the receiver once, then again as long the value of conditionBlock is false."
 
	| result |
	[result _ self value.
	conditionBlock value] whileFalse.

	^ result