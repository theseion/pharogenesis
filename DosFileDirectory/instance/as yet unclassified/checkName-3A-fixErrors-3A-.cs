checkName: aFileName fixErrors: fixing
	"Check if the file name contains any invalid characters"
	| fName badChars hasBadChars |
	fName := super checkName: aFileName fixErrors: fixing.
	badChars := #( $: $< $> $| $/ $\ $? $* $") asSet.
	hasBadChars := fName includesAnyOf: badChars.
	(hasBadChars and:[fixing not]) ifTrue:[^self error:'Invalid file name'].
	hasBadChars ifFalse:[^ fName].
	^ fName collect:
		[:char | (badChars includes: char) 
				ifTrue:[$#] 
				ifFalse:[char]]