pixelAt: aPoint put: pixelValue
	"Assumes this BitBlt has been set up specially (see the init message,
	BitBlt bitPokerToForm:.  Overwrites the pixel at aPoint."
	destX _ aPoint x.
	destY _ aPoint y.
	sourceForm bits at: 1 put: pixelValue.
	self copyBits
"
| bb |
bb _ (BitBlt bitPokerToForm: Display).
[Sensor anyButtonPressed] whileFalse:
	[bb pixelAt: Sensor cursorPoint put: 55]
"