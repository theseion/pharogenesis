typicalFileExtensions
	"Answer a collection of file extensions (lowercase) which files that I can 
	read might commonly have"

	self
		allSubclasses detect: [:cls | cls wantsToHandleGIFs ]
					 ifNone: ["if none of my subclasses wants , then i''ll have to do"
							^ #('gif' )].
	^ #( )