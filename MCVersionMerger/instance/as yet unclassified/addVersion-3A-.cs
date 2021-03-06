addVersion: aVersion
	| dep |
	records add: (MCMergeRecord version: aVersion).
	aVersion dependencies do:
		[:ea |
		dep := ea resolve.
		(records anySatisfy: [:r | r version = dep]) ifFalse: [self addVersion: dep]]