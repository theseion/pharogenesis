poolUsers
	"Answer a dictionary of pool name -> classes that refer to it.
	Also includes any globally know dictionaries (such as
	Smalltalk, Undeclared etc) which although not strictly
	accurate is potentially useful information"
	"Smalltalk poolUsers"
	| poolUsers pool refs |
	poolUsers := Dictionary new.
	self keys
		do: [:k | "yes, using isKindOf: is tacky but for reflective code like
			this it is very useful. If you really object you can:-
			a) go boil your head.
			b) provide a better answer.
			your choice."
			(((pool := self at: k) isKindOf: Dictionary)
					or: [pool isKindOf: SharedPool class])
				ifTrue: [refs := self systemNavigation allClasses
								select: [:c | c sharedPools identityIncludes: pool]
								thenCollect: [:c | c name].
					refs
						add: (self systemNavigation
								allCallsOn: (self associationAt: k)).
					poolUsers at: k put: refs]].
	^ poolUsers