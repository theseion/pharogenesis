categoryOfCurrentMethod
	"Determine the method category associated with the receiver at the current moment, or nil if none"

	| aCategory |
	^ super categoryOfCurrentMethod ifNil:
		[(aCategory _ self messageCategoryListSelection) == ClassOrganizer allCategory
					ifTrue:
						[nil]
					ifFalse:
						[aCategory]]