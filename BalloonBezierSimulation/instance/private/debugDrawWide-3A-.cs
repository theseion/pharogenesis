debugDrawWide: n
	| entry minY maxY canvas curve p1 p2 entry2 y |
	curve _ self class new.
	curve start: start + (0@n).
	curve via: via + (0@n).
	curve end: end + (0@n).
	entry _ BalloonEdgeData new.
	entry2 _ BalloonEdgeData new.
	canvas _ Display getCanvas.
	minY _ (start y min: end y) min: via y.
	maxY _ (start y max: end y) max: via y.
	entry yValue: minY.
	entry2 yValue: minY + n.
	self stepToFirstScanLineAt: minY in: entry.
	curve stepToFirstScanLineAt: minY+n in: entry2.
	y _ minY.
	1 to: n do:[:i|
		y _ y + 1.
		self stepToNextScanLineAt: y in: entry.
		p1 _ entry xValue @ y.
		canvas line: p1 to: p1 + (n@0) width: 1 color: Color black.
	].
	[y < maxY] whileTrue:[
		y _ y + 1.
		self stepToNextScanLineAt: y in: entry.
		p2 _ (entry xValue + n) @ y.
		curve stepToNextScanLineAt: y in: entry2.
		p1 _ entry2 xValue @ y.
		canvas line: p1 to: p2 width: 1 color: Color black.
	].
