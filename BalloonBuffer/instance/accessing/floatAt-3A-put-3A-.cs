floatAt: index put: value
	"For simulation only"
	<primitive: 'primitiveFloatArrayAtPut'>
	value isFloat 
		ifTrue:[self basicAt: index put: value asIEEE32BitWord]
		ifFalse:[self at: index put: value asFloat].
	^value