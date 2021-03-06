addSimulatedMembers
	| membersBySlantAndStretch heaviest membersByWeightAndStretch regular oblique |
	membersBySlantAndStretch := Dictionary new.
	members do:[:each| 
		(membersBySlantAndStretch 
			at: {each slantValue. each stretchValue} 
			ifAbsentPut:[OrderedCollection new]) 
				add: each].
	membersBySlantAndStretch keysAndValuesDo:[:key :col |
		heaviest := col ifNotEmpty:[col first].
		col do:[:each |
			heaviest weightValue < each weightValue
				ifTrue:[heaviest := each]].
		(heaviest weightValue between: (LogicalFont weightRegular - 50) and: (LogicalFont weightMedium + 50))
			ifTrue:[	members add: heaviest asSimulatedBold]].			
	membersByWeightAndStretch := Dictionary new.
	members do:[:each| | normalizedWeight |
		normalizedWeight := each weightValue.
		each weightValue = LogicalFont weightMedium ifTrue:[normalizedWeight := LogicalFont weightRegular].	
		"regular and medium weights are used interchangeably.
		For example, FreeSans has Regular-weightMedium(500), and Oblique-weightRegular(400).
		We don't want to simulate oblique-weightMedium(500) when a real 
		Oblique-weightMedium(500) exists, so we normalize any weightMedium(500)
		values to weightRegular(400) to prevent this happening" 	
		(membersByWeightAndStretch 
			at: {normalizedWeight. each stretchValue} 
			ifAbsentPut:[OrderedCollection new]) 
				add: each].	
	membersByWeightAndStretch keysAndValuesDo:[:key :col |
		regular := col detect: [:each | each slantValue = 0] ifNone:[].
		oblique := col detect:[:each | each slantValue > 0] ifNone:[]. "oblique or italic"
		(oblique isNil and:[regular notNil]) 
			ifTrue:[
				regular simulated
					ifTrue:[members add: regular asSimulatedBoldOblique]
					ifFalse:[	members add: regular asSimulatedOblique]]]