paddedSpace
	"Each space is a stop condition when the alignment is right justified. 
	Padding must be added to the base width of the space according to 
	which space in the line this space is and according to the amount of 
	space that remained at the end of the line when it was composed."

	spaceCount := spaceCount + 1.
	destX := destX + spaceWidth + (line justifiedPadFor: spaceCount).
	lastIndex := lastIndex + 1.
	^ false