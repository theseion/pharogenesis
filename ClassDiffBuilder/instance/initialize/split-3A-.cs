split: aString
	| lines in out c |
	lines := OrderedCollection new.
	in := ReadStream on: aString.
	out := WriteStream on: String new.
	[in atEnd] whileFalse:[
		(c := in next) isSeparator ifTrue:[
			out nextPut: c.
			lines add: out contents.
			out reset.
		] ifFalse:[
			out nextPut: c.
		].
	].
	out position = 0 ifFalse:[
		lines add: out contents.
	].
	^lines