activateReturn: aContext value: value
	"Activate 'aContext return: value', so execution will return to aContext's sender"

	^ suspendedContext := suspendedContext activateReturn: aContext value: value