browseLocalImplementors
	"Present a menu of all messages sent by the currently selected message. 
	Open a message set browser of all implementors of the message chosen in or below
	the selected class.
	Do nothing if no message is chosen."
	self getSelectorAndSendQuery: #browseAllImplementorsOf:localTo:
		to: self systemNavigation
		with: { self selectedClass }