drawZoomOn: aCanvas
	"Zoom in: endForm expands overlaying startForm.
	Zoom out: startForm contracts revealing endForm."
	| box innerForm outerForm boxExtent scale |
	direction = #in
		ifTrue: [innerForm _ endForm.  outerForm _ startForm.
				boxExtent _ self stepFrom: 0@0 to: self extent]
		ifFalse: [innerForm _ startForm.  outerForm _ endForm.
				boxExtent _ self stepFrom: self extent to: 0@0].

	aCanvas drawImage: outerForm at: self position.

	box _ Rectangle center: self center extent: boxExtent.
	scale _ box extent asFloatPoint / bounds extent.
	aCanvas drawImage: (innerForm magnify: innerForm boundingBox by: scale smoothing: 1)
		at: box topLeft.

	((box expandBy: 1) areasOutside: box) do:
		[:r | aCanvas fillRectangle: r color: Color black].
