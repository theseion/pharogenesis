drawPolygon: vertices color: aColor borderWidth: bw borderColor: bc 
	| fillC |
	fillC _ self shadowColor ifNil:[aColor].
	self
		preserveStateDuring: [:pc | pc
			 outlinePolygon: vertices;
				 setLinewidth: bw;
				
				fill: fillC
				andStroke: ((bc isKindOf: Symbol)
						ifTrue: [Color gray]
						ifFalse: [bc])]