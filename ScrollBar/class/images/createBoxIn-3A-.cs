createBoxIn: aRectangle 
	"PRIVATE - create an box bounded in aRectangle"
	| box |
	box := RectangleMorph new.
	box extent: (aRectangle scaleBy: 1 / 2) extent rounded;
		 borderWidth: 0.
	""
	^ box