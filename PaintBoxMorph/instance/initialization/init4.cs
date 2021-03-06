init4
	"Just a record of how Ted loaded in the paintbox button images, Feb 98"
| bb im pp newImage pic24Bit picNewBit blt |

"self loadoffImage: 'roundedPalette3.bmp'."
pic24Bit := GIFReadWriter formFromServerFile: 'updates/137roundedPalette3.bmp'.
picNewBit := Form extent: pic24Bit extent depth: 16.
pic24Bit displayOn: picNewBit.
OriginalBounds := picNewBit boundingBox.
AllOffImage := Form extent: OriginalBounds extent depth: 16.
blt := BitBlt current toForm: AllOffImage.
blt sourceForm: picNewBit; combinationRule: Form over;
		sourceRect: OriginalBounds; destOrigin: 0@0; copyBits.

AllOffImage mapColor: Color transparent to: Color black.
self image: AllOffImage.
self invalidRect: bounds.

self submorphsDo: [:button | button position: button position + (10@10)].
(im := submorphs at: 28) class == ImageMorph ifTrue: [
	im position: im position + (2@0)].	"color picker"
"exercise it once"

(bb := self submorphNamed: #keep:) position: bb position + (0@25).
(bb := self submorphNamed: #toss:) position: bb position + (0@25).
(bb := self submorphNamed: #undo:) position: bb position + (0@-25).
(bb := self submorphNamed: #clear:) position: bb position + (0@-25).
(bb := self submorphNamed: #undo:) position: bb position + (0@-69).
(bb := self submorphNamed: #clear:) position: bb position + (0@-69).
self submorphsDo: [:button | 
	button class == AlignmentMorph ifTrue: [
		button position: button position + (0@25)].
	(button printString includesSubString: 'stamp:') ifTrue: [
		button position: button position + (0@25)]].
(bb := self submorphNamed: #prevStamp:) position: bb position + (0@25).
(bb := self submorphNamed: #nextStamp:) position: bb position + (0@25).

bb := self submorphNamed: #keep:.
newImage := bb pressedImage copy: (0@4 corner: (bb pressedImage boundingBox extent)).
bb onImage: newImage.  bb pressedImage: newImage.  bb extent: newImage extent.
bb position: bb position + (4@1).

pp := (bb := self submorphNamed: #toss:) pressedImage.
newImage := pp copy: (0@4 corner: (bb pressedImage extent - (3@0))).
bb onImage: newImage.  bb pressedImage: newImage.  
bb extent: newImage extent.
bb position: bb position + (3@1).

pp := (bb := self submorphNamed: #undo:) pressedImage.
newImage := pp copy: (0@0 corner: (bb pressedImage extent - (3@5))).
bb onImage: newImage.  bb pressedImage: newImage.  
bb extent: newImage extent.
bb position: bb position + (3@-1).

pp := (bb := self submorphNamed: #clear:) pressedImage.
newImage := pp copy: (0@0 corner: (bb pressedImage extent - (0@5))).
bb onImage: newImage.  bb pressedImage: newImage.  
bb extent: newImage extent.
bb position: bb position + (3@-1).

pic24Bit := GIFReadWriter formFromServerFile: 'updates/137pencil.bmp'.
picNewBit := Form extent: pic24Bit extent depth: 16.
pic24Bit displayOn: picNewBit.
newImage := picNewBit as8BitColorForm.
newImage transparentColor: (Color r: 0 g: 0 b: 0).
(bb := self submorphNamed: #erase:) pressedImage: newImage; onImage: newImage;
	extent: newImage extent.

bb position: bb position + (-11@-1).
