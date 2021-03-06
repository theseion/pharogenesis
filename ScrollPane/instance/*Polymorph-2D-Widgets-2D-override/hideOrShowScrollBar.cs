hideOrShowScrollBar
	"Hide or show the scrollbar depending on if the pane is scrolled/scrollable."

	"Don't do anything with the retractable scrollbar unless we have focus"
	retractableScrollBar & self hasFocus not ifTrue: [^self].
	"Don't show it if we were told not to."
	(self valueOfProperty: #noScrollBarPlease ifAbsent: [false]) ifTrue: [^self].

	self vIsScrollbarNeeded not & self isScrolledFromTop not ifTrue: [self vHideScrollBar].
	self vIsScrollbarNeeded | self isScrolledFromTop ifTrue: [self vShowScrollBar].
