mouseMove: evt
	"Handle a mouse-move event"

	| cp |
	cp := evt cursorPoint.
	self valueOfProperty: #clickPoint ifPresentDo: 
		[:firstClick |
		((self labelRect containsPoint: firstClick) and: [(cp dist: firstClick) > 3]) ifTrue:
		["If this is a drag that started in the title bar, then pick me up"
		^ self isSticky ifFalse:
			[self fastFramingOn 
				ifTrue: [self doFastFrameDrag: firstClick] "pass the first click."
				ifFalse: [evt hand grabMorph: self topRendererOrSelf]]]].
	model windowActiveOnFirstClick ifTrue:
		["Normally window takes control on first click.
		Need explicit transmission for first-click activity."
		submorphs do: [:m | (m containsPoint: cp) ifTrue: [m mouseMove: evt]]]