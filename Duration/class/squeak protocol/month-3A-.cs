month: aMonth
	"aMonth is an Integer or a String"
	
	^ (Month month: aMonth year: Year current year) duration
