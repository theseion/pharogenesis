testMethodContext
	self deny: aMethodContext isPseudoContext.
	self assert: aMethodContext home notNil.
	self assert: aMethodContext receiver notNil.
	self assert: (aMethodContext method isKindOf: CompiledMethod).