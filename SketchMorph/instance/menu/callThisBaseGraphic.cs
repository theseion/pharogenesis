callThisBaseGraphic
	"Set my baseGraphic to be the current form"

	| aGraphic |
	self isInWorld ifFalse: [^ self inform: 

'oops, this menu is a for a morph that
has been replaced, probably because a
"look like" script was run.  Please dismiss
the menu and get a new one!.  Sorry!' translated].

	((aGraphic := self valueOfProperty: #baseGraphic)
				notNil and: [aGraphic ~= originalForm])
		ifTrue:
			[self setProperty: #baseGraphic toValue: originalForm]
		ifFalse:
			[self inform: 'this already *was* your baseGraphic' translated]