checkForSlips
	"Return a collection of method refs with possible debugging code in them."
	| slips method |
	slips := OrderedCollection new.
	self changedClasses do:
		[:aClass |
		(self methodChangesAtClass: aClass name) associationsDo: 
				[:mAssoc | (#(remove addedThenRemoved) includes: mAssoc value) ifFalse:
					[method := aClass compiledMethodAt: mAssoc key ifAbsent: [nil].
					method ifNotNil:
						[method hasReportableSlip
							ifTrue: [slips add: aClass name , ' ' , mAssoc key]]]]].
	^ slips