doPrimitive: primitiveIndex method: meth receiver: receiver args: arguments 
	"Simulate a primitive method whose index is primitiveIndex.  The
	 simulated receiver and arguments are given as arguments to this message.
	 Any primitive which provikes execution needs to be intercepted and simulated
	 to avoid execution running away."

	| value |
	<primitive: 19> "Simulation guard"
	"If successful, push result and return resuming context,
		else ^ PrimitiveFailToken"
	(primitiveIndex = 19) ifTrue:
		[ToolSet 
			debugContext: self
			label:'Code simulation error'
			contents: nil].

	"ContextPart>>blockCopy:; simulated to get startpc right"
	(primitiveIndex = 80 and: [receiver isKindOf: ContextPart]) 
		ifTrue: [^self push: ((BlockContext newForMethod: receiver method)
						home: receiver home
						startpc: pc + 2
						nargs: (arguments at: 1))].
	(primitiveIndex = 81 and: [receiver isMemberOf: BlockContext]) "BlockContext>>value[:value:...]"
		ifTrue: [^receiver pushArgs: arguments from: self].
	(primitiveIndex = 82 and: [receiver isMemberOf: BlockContext]) "BlockContext>>valueWithArguments:"
		ifTrue: [^receiver pushArgs: arguments first from: self].
	primitiveIndex = 83 "afr 9/11/1998 19:50" "Object>>perform:[with:...]"
		ifTrue: [^self send: arguments first to: receiver
					with: arguments allButFirst
					super: false].
	primitiveIndex = 84 "afr 9/11/1998 19:50" "Object>>perform:withArguments:"
		ifTrue: [^self send: arguments first to: receiver
					with: (arguments at: 2)
					super: false].
	primitiveIndex = 188 ifTrue: "eem 5/27/2008 11:10 Object>>withArgs:executeMethod:"
		[^MethodContext
			sender: self
			receiver: receiver
			method: (arguments at: 2)
			arguments: (arguments at: 1)].

	"Closure primitives"
	(primitiveIndex = 200 and: [receiver == self]) ifTrue:
		"ContextPart>>closureCopy:copiedValues:; simulated to get startpc right"
		[^self push: (BlockClosure
						outerContext: receiver
						startpc: pc + 2
						numArgs: arguments first
						copiedValues: arguments last)].
	((primitiveIndex between: 201 and: 205)			 "BlockClosure>>value[:value:...]"
	or: [primitiveIndex between: 221 and: 222]) ifTrue: "BlockClosure>>valueNoContextSwitch[:]"
		[^receiver simulateValueWithArguments: arguments caller: self].
	primitiveIndex = 206 ifTrue:						"BlockClosure>>valueWithArguments:"
		[^receiver simulateValueWithArguments: arguments first caller: self].

	primitiveIndex = 120 ifTrue:[ "FFI method"
		value := meth literals first tryInvokeWithArguments: arguments.
	] ifFalse:[
		arguments size > 6 ifTrue: [^PrimitiveFailToken].
		value := primitiveIndex = 117 "named primitives"
				ifTrue:[self tryNamedPrimitiveIn: meth for: receiver withArgs: arguments]
				ifFalse:[receiver tryPrimitive: primitiveIndex withArgs: arguments].
	].
	^value == PrimitiveFailToken
		ifTrue: [PrimitiveFailToken]
		ifFalse: [self push: value]