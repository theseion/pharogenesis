testOneArgument
	| c |
	c _ OrderedCollection new.
	c add: 'hello'.
	[c
		do: [1 + 2]]
		ifError: [:err :rcvr | self deny: err = 'This block requires 0 arguments.'].
	[c
		do: [:arg1 :arg2 | 1 + 2]]
		ifError: [:err :rcvr | self deny: err = 'This block requires 2 arguments.'] 