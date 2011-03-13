testEmptyError

	| aStack |
	aStack := Stack new.
	self should: [ aStack top ] raise: Error.
	self should: [ aStack pop] raise: Error.
	
	aStack push: 'element'.
	
	self shouldnt: [ aStack top ] raise: Error.
	self shouldnt: [ aStack pop] raise: Error.
	
	
	"The stack is empty again due to previous pop"
	self should: [ aStack top ] raise: Error.
	self should: [ aStack pop] raise: Error.