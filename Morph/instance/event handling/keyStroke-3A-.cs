keyStroke: anEvent
	"Handle a keystroke event.  The default response is to let my eventHandler, if any, handle it."

	eventHandler ifNotNil:
		[eventHandler keyStroke: anEvent fromMorph: self].
