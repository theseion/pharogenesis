testCreationRandom
	| uuid |

	(UUID new asString last: 12) = (UUID new asString last: 12) ifTrue: [^self].
	1000 timesRepeat:
		[uuid _ UUID new.
		self should: [((uuid at: 7) bitAnd: 16rF0) = 16r40].
		self should: [((uuid at: 9) bitAnd: 16rC0) = 16r80]]
