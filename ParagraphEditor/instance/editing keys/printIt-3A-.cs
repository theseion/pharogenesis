printIt: characterStream 
	"Print the results of evaluting the selection -- invoked via cmd-p.  If there is no current selection, use the current line.  1/17/96 sw
	 2/29/96 sw: don't call selectLine now, since it's called by doIt"

	sensor keyboard.		"flush character"
	self terminateAndInitializeAround: [self printIt].
	^ true