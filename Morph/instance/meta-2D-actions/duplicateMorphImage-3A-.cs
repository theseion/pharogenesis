duplicateMorphImage: evt 
	"Make and return a imageMorph of the receiver's argument imageForm"
	| dup |
	dup := self asSnapshotThumbnail withSnapshotBorder.
	dup bounds: self bounds.
	evt hand grabMorph: dup from: owner.
	"duplicate was ownerless so use #grabMorph:from: here"
	^ dup