withoutTrailingBlanks
	"Return a copy of the receiver from which trailing blanks have been trimmed."

	| last |
	last _ self findLast: [:c | c isSeparator not].
	last = 0 ifTrue: [^ ''].  "no non-separator character"
	^ self copyFrom: 1 to: last

	" ' abc  d   ' withoutTrailingBlanks"
