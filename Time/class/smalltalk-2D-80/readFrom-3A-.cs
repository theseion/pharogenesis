readFrom: aStream
	"Read a Time from the stream in the form:
		<hour>:<minute>:<second> <am/pm>

	<minute>, <second> or <am/pm> may be omitted.  e.g. 1:59:30 pm; 8AM; 15:30"

	| hour minute second ampm |
	hour _ Integer readFrom: aStream.
	minute _ 0.
	second _ 0.
	(aStream peekFor: $:) ifTrue:
	
	[ minute _ Integer readFrom: aStream.
		(aStream peekFor: $:) ifTrue: [ second _ Integer readFrom: aStream ]].
	aStream skipSeparators.
	(aStream atEnd not and: [aStream peek isLetter]) ifTrue: 
		[ampm _ aStream next asLowercase.
	
	(ampm = $p and: [hour < 12]) ifTrue: [hour _ hour + 12].
		(ampm = $a and: [hour = 12]) ifTrue: [hour _ 0].
	
	(aStream peekFor: $m) ifFalse: [aStream peekFor: $M ]].

	^ self hour: hour minute: minute second: second

	"Time readFrom: (ReadStream on: '2:23:09 pm')"
