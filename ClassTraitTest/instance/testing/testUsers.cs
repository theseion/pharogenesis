testUsers
	self assert: self t2 classSide users size = 3.
	self assert: (self t2 classSide users includesAllOf: {				
		(self t4 classTrait).
		(self t5 classTrait).
		(self t6 classTrait) }).
	self assert: self t5 classSide users size = 1.
	self assert: self t5 classSide users anyOne = self c2 class.
	self c2 setTraitCompositionFrom: self t1 + self t5.
	self assert: self t5 classSide users size = 1.
	self assert: self t5 classSide users anyOne = self c2 class.
	self c2 setTraitComposition: self t2 asTraitComposition.
	self assert: self t5 classSide users isEmpty