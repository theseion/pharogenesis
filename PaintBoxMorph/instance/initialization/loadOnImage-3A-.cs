loadOnImage: fileName
	"Read in and convert the image for the paintBox with the buttons
on.  A .bmp 24-bit image.  For each button, cut that chunk out and save it."
	"	self loadOnImage: 'NoSh_on.bmp'.
		AllOnImage _ nil.	'save space'.	"

	| pic16Bit blt aa on type |
	type _ 'gif'.  "   gif or bmp  "
type = 'gif' ifTrue: [
	pic16Bit "really 8" _ GIFReadWriter formFromFileNamed: fileName.
	pic16Bit display.
	aa _ AllOnImage _ Form extent: OriginalBounds extent depth: 8.
	blt _ BitBlt toForm: aa.
	blt sourceForm: pic16Bit; combinationRule: Form over;
		sourceRect: OriginalBounds; destOrigin: 0@0; copyBits.
	].
type = 'bmp' ifTrue: [
	pic16Bit _ Form fromBMPFileNamed: fileName depth: 16.
	pic16Bit display.
	aa _ AllOnImage _ Form extent: OriginalBounds extent depth: 16.
	blt _ BitBlt toForm: aa.
	blt sourceForm: pic16Bit; combinationRule: Form over;
		sourceRect: OriginalBounds; destOrigin: 0@0; copyBits.
	aa removeZeroPixelsFromForm.
	].
	"Collect all the images for the buttons in the on state"
	self allMorphsDo: [:button |
		(button isKindOf: ThreePhaseButtonMorph) ifTrue: [
			type = 'gif' ifTrue: [on _ ColorForm extent: button extent depth: 8.
					 on colors: pic16Bit colors]
				ifFalse: [on _ Form extent: button extent depth: 16].
			on copy: (0@0 extent: button extent)
				from: (button topLeft - self topLeft) in: aa rule: Form over.
			button onImage: on]].
	self invalidRect: bounds.

	