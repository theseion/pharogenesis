calibrateScrollbar
	"Set the scrollbar parameters to match the texts."

	|maxY range delta innerH|
	self fullBounds.
	maxY := self srcMorph textExtent y max: self dstMorph textExtent y.
	innerH := self dstMorph innerBounds height.
	delta := self dstMorph textMorph defaultLineHeight.
	range := maxY - innerH max: 0.
	range = 0 ifTrue: [^self scrollbarMorph scrollDelta: 0.02 pageDelta: 0.2; interval: 1.0; setValue: 0.0].
	self scrollbarMorph
		scrollDelta: (delta / range) asFloat 
		pageDelta: ((innerH - delta) / range) asFloat;
		interval: (innerH / maxY) asFloat;
		setValue: ((self srcMorph scroller offset y max: self dstMorph scroller offset y)
					 / range min: 1.0) asFloat