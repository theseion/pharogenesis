initialize
	"initialize the state of the receiver"
	| f |
	super initialize.
	""

	f _ Form extent: 60 @ 80 depth: Display depth.
	f fill: f boundingBox fillColor: color.
	self form: f