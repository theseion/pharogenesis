errorReportOn: strm
	"Write a detailed error report on the stack (above me) on a  
	stream.  For both the error file, and emailing a bug report.   
	Suppress any errors while getting printStrings.  Limit the length."
	
	| cnt aContext startPos |
	strm print: Date today; space; print: Time now; cr.
	strm cr.
	strm nextPutAll: 'VM: ';
		nextPutAll:  SmalltalkImage current platformName asString;
		nextPutAll: ' - ';	
		nextPutAll: SmalltalkImage current platformSubtype asString;
		nextPutAll: ' - ';
		nextPutAll: SmalltalkImage current osVersion asString;
		nextPutAll: ' - ';
		nextPutAll: SmalltalkImage current vmVersion asString;
		cr.	
	strm nextPutAll: 'Image: ';
		nextPutAll:  SystemVersion current version asString;
		nextPutAll: ' [';
		nextPutAll: SmalltalkImage current lastUpdateString asString;
		nextPutAll: ']';
		cr.
	strm cr.
	SecurityManager default printStateOn: strm.
		"Note: The following is an open-coded version of  ContextPart>>stackOfSize: since this method may be called during a  low space condition and we might run out of space for allocating the  full stack."	
	cnt := 0.  
	startPos := strm position.
	aContext := self.
	[aContext notNil and: [(cnt := cnt + 1) < 40]] 
		whileTrue: [aContext printDetails: strm.	"variable values"
					strm cr.
					aContext := aContext sender].
	strm cr; nextPutAll: '--- The full stack ---'; cr.
	aContext := self.	
	cnt := 0.
	[aContext == nil] whileFalse:	
		[cnt := cnt + 1.	
		cnt = 40 ifTrue: [strm nextPutAll: ' - - - - - - - - - - - - - - -  
			- - - - - - - - - - - - - - - - - -'; cr].
		strm print: aContext; cr.  "just class>>selector"			
		strm position > (startPos+150000) 
			ifTrue: [strm nextPutAll:  '...etc...'.			
					^ self]. 	"exit early"	
		cnt > 200 ifTrue: [strm nextPutAll: '-- and more not shown --'.  ^  self].
		aContext := aContext sender].