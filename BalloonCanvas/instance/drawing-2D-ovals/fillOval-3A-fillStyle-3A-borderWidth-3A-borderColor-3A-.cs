fillOval: aRectangle fillStyle: aFillStyle borderWidth: bw borderColor: bc
	"Fill the given rectangle."
	^self drawOval: (aRectangle insetBy: bw // 2)
			color: aFillStyle "@@: Name confusion!!!"
			borderWidth: bw
			borderColor: bc
