innerBounds
	| inner |
	inner _ super innerBounds.
	retractableScrollBar | (submorphs includes: scrollBar) not ifFalse:[
		inner _ (scrollBarOnLeft
					ifTrue: [scrollBar right @ inner top corner: inner bottomRight]
					ifFalse: [inner topLeft corner: scrollBar left @ inner bottom])
	].
	(retractableScrollBar | self hIsScrollbarShowing not)
		ifTrue: [^ inner]
		ifFalse: [^ inner topLeft corner: (inner bottomRight - (0@self scrollBarThickness))].
