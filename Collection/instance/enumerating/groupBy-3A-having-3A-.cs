groupBy: keyBlock having: selectBlock 
	"Like in SQL operation - Split the recievers contents into collections of 
	elements for which keyBlock returns the same results, and return those 
	collections allowed by selectBlock. keyBlock should return an Integer."
	| result key |
	result := PluggableDictionary integerDictionary.
	self do: 
		[:e | 
		key := keyBlock value: e.
		(result includesKey: key)
			ifFalse: [result at: key put: OrderedCollection new].
		(result at: key)
			add: e].
	^ result := result select: selectBlock