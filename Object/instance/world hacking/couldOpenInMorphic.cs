couldOpenInMorphic
	"is there an obvious morphic world in which to open a new morph?"
	self deprecated: #mvcIsRemoved.
	^ World notNil
		or: [ActiveWorld notNil]