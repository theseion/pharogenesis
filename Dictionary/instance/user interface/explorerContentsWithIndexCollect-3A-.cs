explorerContentsWithIndexCollect: twoArgBlock

	| sortedKeys |
	sortedKeys := self keys asSortedCollection: [:x :y |
		((x isString and: [y isString])
			or: [x isNumber and: [y isNumber]])
			ifTrue: [x < y]
			ifFalse: [x class == y class
				ifTrue: [x printString < y printString]
				ifFalse: [x class name < y class name]]].
	^ sortedKeys collect: [:k | twoArgBlock value: (self at: k) value: k].
