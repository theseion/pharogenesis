quickAddAllMorphs: aCollection
"A fast add of all the morphs for the PluggableListMorph>>list: method to use -- assumes that fullBounds will get called later by the sender, so it avoids doing any updating on the morphs in aCol or updating layout of this scroller. So the sender should handle those tasks as appropriate"

	| myWorld itsWorld |
	myWorld _ self world.
	aCollection do: [:m |
		m owner ifNotNil: [
			itsWorld _ m world.
			itsWorld == myWorld ifFalse: [m outOfWorld: itsWorld].
			m owner privateRemoveMorph: m].
		m privateOwner: self.
		"inWorld ifTrue: [self addedOrRemovedSubmorph: m]."
		itsWorld == myWorld ifFalse: [m intoWorld: myWorld].
		].
	submorphs _ aCollection.
	"self layoutChanged."

