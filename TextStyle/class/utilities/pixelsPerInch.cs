pixelsPerInch
	"Answer the nominal resolution of the screen."

	^TextConstants at: #pixelsPerInch ifAbsentPut: [ 96.0 ].