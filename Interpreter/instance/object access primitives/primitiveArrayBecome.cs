primitiveArrayBecome
	"We must flush the method cache here, to eliminate stale references
	to mutated classes and/or selectors."

	| arg rcvr |
	arg _ self stackTop.
	rcvr _ self stackValue: 1.
	self success: (self become: rcvr with: arg twoWay: true).
	self flushMethodCache.
	successFlag ifTrue: [ self pop: 1 ].