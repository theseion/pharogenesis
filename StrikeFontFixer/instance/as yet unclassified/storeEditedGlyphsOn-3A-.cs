storeEditedGlyphsOn: aStream

	| n |
	NoFontTable do: [:i |
		n _ strikeFont name.
		(n beginsWith: 'NewYork') ifTrue: [n _ 'NewYork'].
		aStream nextPutAll: '((StrikeFont familyName: ''', n, ''' size: ',
			strikeFont height asString, ')'.
		aStream nextPutAll: ' characterFormAt: '.
		aStream nextPutAll: '(Character value: ', i asString, ')'.
		aStream nextPutAll: ' put: '.
		(strikeFont characterFormAt: (Character value: i)) storeOn: aStream base: 2.
		aStream nextPutAll: ')!'.
		aStream nextPut: Character cr.
		aStream nextPut: Character cr.
	].
