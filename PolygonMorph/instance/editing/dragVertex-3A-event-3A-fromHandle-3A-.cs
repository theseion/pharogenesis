dragVertex: ix event: evt fromHandle: handle
	| p |
	p _ self isCurve
		ifTrue: [evt cursorPoint]
		ifFalse: [self griddedPoint: evt cursorPoint].
	handle position: p - (handle extent//2).
	self verticesAt: ix put: p.
