deleteCurrentStamp
	"The trash is telling us to delete the currently selected stamp"

	(tool arguments at: 2) == #stamp: ifTrue: [
		stampHolder remove: tool.
		self setAction: #paint:].	"no use stamping with a blank stamp"