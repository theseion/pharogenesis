leapYear: yearInteger 

	^ (self isLeapYear: yearInteger)
		ifTrue: [1]
		ifFalse: [0]