copyUpOrCopyDown
	"Used to copy down code from a superclass to a subclass or vice-versa in one easy step, if you know what you're doing.  Prompt the user for which class to copy down or copy up to, then spawn a fresh browser for that class, with the existing code planted in it, and with the existing method category also established."

	| aClass aSelector allClasses implementors aMenu aColor |
	((aClass := self selectedClassOrMetaClass) isNil or: [(aSelector := self selectedMessageName) == nil]) 
		ifTrue:	[^ Beeper beep].

	allClasses := self systemNavigation hierarchyOfClassesSurrounding: aClass.
	implementors := self systemNavigation hierarchyOfImplementorsOf: aSelector forClass: aClass.
	aMenu := MenuMorph new defaultTarget: self.
	aMenu title: 
aClass name, '.', aSelector, '
Choose where to insert a copy of this method
(blue = current, black = available, red = other implementors'.
	allClasses do:
		[:cl |
			aColor := cl == aClass
				ifTrue:	[#blue]
				ifFalse:
					[(implementors includes: cl)
						ifTrue:	[#red]
						ifFalse:	[#black]].
			(aColor == #red)
				ifFalse:
					[aMenu add: cl name selector: #spawnToClass: argument: cl]
				ifTrue:
					[aMenu add: cl name selector: #spawnToCollidingClass: argument: cl].
			aMenu lastItem color: (Color colorFrom: aColor)].
	aMenu popUpInWorld