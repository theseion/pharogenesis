paintImage: aForm at: aPoint sourceRect: sourceRect
	"Draw the given Form, which is assumed to be a Form or ColorForm following the convention that zero is the transparent pixel value."
	self shadowColor ifNotNil:[
		^self stencil: aForm at: aPoint sourceRect: sourceRect color: self shadowColor].
	^self image: aForm
		at: aPoint
		sourceRect: sourceRect
		rule: Form paint