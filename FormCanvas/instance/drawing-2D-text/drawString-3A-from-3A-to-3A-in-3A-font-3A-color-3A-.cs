drawString: aString from: firstIndex to: lastIndex in: bounds font: fontOrNil color: c
	| font portRect |
	port colorMap: nil.
	portRect := port clipRect.
	port clipByX1: bounds left + origin x 
		y1: bounds top + origin y 
		x2: bounds right + origin x 
		y2: bounds bottom + origin y.
	font := fontOrNil ifNil: [TextStyle defaultFont].
	port combinationRule: Form paint.
	font installOn: port
		foregroundColor: (self shadowColor ifNil:[c]) 
		backgroundColor: Color transparent.
	font displayString: aString asString on: port 
		from: firstIndex to: lastIndex at: (bounds topLeft + origin) kern: 0.
	port clipRect: portRect.