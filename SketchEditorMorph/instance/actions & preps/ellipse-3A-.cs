ellipse: evt
	"Draw an ellipse from the center. "

	| rect oldRect ww ext oldExt cColor sOrigin priorEvt |

	sOrigin _ self get: #strokeOrigin for: evt.
	cColor _ self getColorFor: evt.
	ext _ (sOrigin - evt cursorPoint) abs * 2.
	evt shiftPressed ifTrue: [ext _ self shiftConstrainPoint: ext].
	rect _ Rectangle center: sOrigin extent: ext.
	ww _ (self getNibFor: evt) width.
	(priorEvt _ self get: #lastEvent for: evt) ifNotNil: [
		oldExt _ (sOrigin - priorEvt cursorPoint) abs + ww * 2.
		priorEvt shiftPressed ifTrue: [oldExt _ self shiftConstrainPoint: oldExt].
		(oldExt < ext) ifFalse: ["Last draw sticks out, must erase the area"
			oldRect _ Rectangle center: sOrigin extent: oldExt.
			self restoreRect: oldRect]].
	cColor == Color transparent
		ifFalse:
			[formCanvas fillOval: rect color: Color transparent borderWidth: ww borderColor: cColor]
		ifTrue:
			[formCanvas fillOval: rect color: cColor borderWidth: ww borderColor: Color black].

	self invalidRect: rect.

