layout: aMorph in: newBounds
	"Compute the layout for the given morph based on the new bounds"
	aMorph submorphsDo:[:m| m layoutProportionallyIn: newBounds].