test0FixtureCopyWithReplacementTest

	self shouldnt: [self replacementCollection   ]raise: Error.
	self shouldnt: [self oldSubCollection]  raise: Error.
	
	self shouldnt: [self collectionWith1TimeSubcollection ]raise: Error.
	self assert: (self howMany: self oldSubCollection  in: self collectionWith1TimeSubcollection  ) = 1.
	
	