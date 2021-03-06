displayZoom: entering
	"Show the project transition when entering a new project"
	| newDisplay vanishingPoint |

	"Show animated zoom to new display"
	newDisplay := self imageForm.
	entering
		ifTrue: [vanishingPoint := Sensor cursorPoint]
		ifFalse: [vanishingPoint := self viewLocFor: CurrentProject].
	Display zoomIn: entering orOutTo: newDisplay at: 0@0
			vanishingPoint: vanishingPoint.