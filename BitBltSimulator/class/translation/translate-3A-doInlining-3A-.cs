translate: fileName doInlining: inlineFlag
	"Time millisecondsToRun: [
		Interpreter translate: 'interp.c' doInlining: true.
		Smalltalk beep] 164760 167543 171826 174510"
	| cg |
	BitBltSimulation initialize.
	Interpreter initialize.
	ObjectMemory initialize.
	cg _ CCodeGenerator new initialize.
	cg addClass: BitBltSimulation.
	cg addClass: Interpreter.
	cg addClass: ObjectMemory.
	BitBltSimulation declareCVarsIn: cg.
	Interpreter declareCVarsIn: cg.
	ObjectMemory declareCVarsIn: cg.
	cg storeCodeOnFile: fileName doInlining: inlineFlag.