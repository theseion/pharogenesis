isBreakableAt: index in: text

	| prev |
	index = 1 ifTrue: [^ false].
	prev _ text at: index - 1.
	prev leadingChar ~= 1 ifTrue: [^ true].
	^ false
