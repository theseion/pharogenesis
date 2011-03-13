newFrom: aCollection 
	"Answer an instance of me containing the same elements as aCollection."

	| newCollection |
	newCollection := self new.
	aCollection do: [:x | newCollection addLast: x].
	^newCollection

"	RunArray newFrom: {1. 2. 2. 3}
	{1. $a. $a. 3} as: RunArray
	({1. $a. $a. 3} as: RunArray) values
"