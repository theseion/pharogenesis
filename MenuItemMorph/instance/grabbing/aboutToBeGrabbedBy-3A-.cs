aboutToBeGrabbedBy: aHand
	"Don't allow the receiver to act outside a Menu"
	| menu box |
	(owner notNil and:[owner submorphs size = 1]) ifTrue:[
		"I am a lonely menuitem already; just grab my owner"
		owner stayUp: true.
		^owner 	aboutToBeGrabbedBy: aHand].
	box _ self bounds.
	menu _ MenuMorph new defaultTarget: nil.
	menu addMorphFront: self.
	menu bounds: box.
	menu stayUp: true.
	self isSelected: false.
	^menu