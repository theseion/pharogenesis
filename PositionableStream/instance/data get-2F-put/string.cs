string
	"Answer the next string from this (binary) stream."

	| size |
	size _ self uint16.
	^ (self next: size) asString
