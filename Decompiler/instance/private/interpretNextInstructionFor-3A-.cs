interpretNextInstructionFor: client

	| code varNames |

"Change false here will trace all state in Transcript."
true ifTrue: [^ super interpretNextInstructionFor: client].

	varNames _ Decompiler allInstVarNames.
	code _ (self method at: pc) radix: 16.
	Transcript cr; cr; print: pc; space;
		nextPutAll: '<' , (code copyFrom: 4 to: code size) , '>'.
	8 to: varNames size do:
		[:i | i <= 10 ifTrue: [Transcript cr]
				ifFalse: [Transcript space; space].
		Transcript nextPutAll: (varNames at: i);
				nextPutAll: ': '; print: (self instVarAt: i)].
	Transcript endEntry.
	^ super interpretNextInstructionFor: client