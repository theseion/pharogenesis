writeForExport: shortName
	"Write the segment on the disk with all info needed to reconstruct it in a new image.  For export.  Out pointers are encoded as normal objects on the disk."

	| fileStream temp |
	state = #activeCopy ifFalse: [self error: 'wrong state'].
	temp := endMarker.
	endMarker := nil.
	fileStream := FileStream newFileNamed: (FileDirectory fileName: shortName extension: self class fileExtension).
	fileStream fileOutClass: nil andObject: self.
		"remember extra structures.  Note class names."
	endMarker := temp.
