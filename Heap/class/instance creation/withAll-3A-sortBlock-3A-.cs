withAll: aCollection sortBlock: sortBlock
	"Create a new heap with all the elements from aCollection"
	^(self basicNew)
		setCollection: aCollection asArray copy tally: aCollection size;
		sortBlock: sortBlock;
		yourself