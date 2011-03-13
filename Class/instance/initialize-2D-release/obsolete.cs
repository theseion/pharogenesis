obsolete
	"Change the receiver and all of its subclasses to an obsolete class."
	self == Object 
		ifTrue: [^self error: 'Object is NOT obsolete'].
	self setName: 'AnObsolete' , self name.
	Object class instSize + 1 to: self class instSize do:
		[:i | self instVarAt: i put: nil]. "Store nil over class instVars."
	self classPool: nil.
	self sharedPools: nil.
	self class obsolete.
	super obsolete.