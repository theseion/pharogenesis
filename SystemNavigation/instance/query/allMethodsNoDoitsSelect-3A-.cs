allMethodsNoDoitsSelect: aBlock 
	"Like allSelect:, but strip out Doits"
	| aCollection |
	aCollection _ SortedCollection new.
	Cursor execute
		showWhile: [self
				allBehaviorsDo: [:class | class
						selectorsDo: [:sel | (sel ~~ #DoIt
									and: [aBlock
											value: (class compiledMethodAt: sel)])
								ifTrue: [aCollection
										add: (MethodReference new setStandardClass: class methodSymbol: sel)]]]].
	^ aCollection