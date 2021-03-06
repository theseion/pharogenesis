intersect: aRectangle 
	"Answer a Rectangle that is the area in which the receiver overlaps with 
	aRectangle. Optimized for speed; old code read:
		^Rectangle 
			origin: (origin max: aRectangle origin)
			corner: (corner min: aRectangle corner)
	"
	| aPoint left right top bottom |
	aPoint := aRectangle origin.
	aPoint x > origin x 
		ifTrue: [ left := aPoint x ]
		ifFalse: [ left := origin x ].
	aPoint y > origin y 
		ifTrue: [ top := aPoint y ]
		ifFalse: [ top := origin y ].
	aPoint := aRectangle corner.
	aPoint x < corner x 
		ifTrue: [ right := aPoint x ]
		ifFalse: [ right := corner x ].
	aPoint y < corner y 
		ifTrue: [ bottom := aPoint y ]
		ifFalse: [ bottom := corner y ].
	^ Rectangle 
		origin: left @ top
		corner: right @ bottom