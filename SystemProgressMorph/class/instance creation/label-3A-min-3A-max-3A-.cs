label: shortDescription min: minValue max: maxValue
	UniqueInstance ifNil: [UniqueInstance := super new].
	^UniqueInstance label: (shortDescription contractTo: 100) min: minValue asFloat max: maxValue asFloat