arrowAction: delta
	"Do what is appropriate when an arrow on the tile is pressed; delta will be +1 or -1"

	| soundChoices index |
	soundChoices _ self soundChoices.
	index _ soundChoices indexOf: literal.
	self literal: (soundChoices atWrap: (index + delta)).
	self playSoundNamed: literal