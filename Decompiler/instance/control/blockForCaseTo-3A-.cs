blockForCaseTo: end
	"Decompile a range of code as in statementsForCaseTo:, but return a block node."
	| exprs block oldBase |
	oldBase _ blockStackBase.
	blockStackBase _ stack size.
	exprs _ self statementsForCaseTo: end.
	block _ constructor codeBlock: exprs returns: lastReturnPc = lastPc.
	blockStackBase _ oldBase.
	lastReturnPc _ -1.  "So as not to mislead outer calls"
	^block