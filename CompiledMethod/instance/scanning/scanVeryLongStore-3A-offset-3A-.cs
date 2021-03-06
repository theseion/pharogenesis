scanVeryLongStore: extension offset: offset
	"Answer whether the receiver contains a long load with the given offset.
	Note that the constant +32 is the known difference between a
	store and a storePop for instVars, and it will always fail on literal variables,
	but these only use store (followed by pop) anyway."
	| scanner |
	scanner := InstructionStream on: self.
	^scanner scanFor:
		[:instr | | ext |
		(instr = 132 and: [(ext := scanner followingByte) = extension
											or: ["might be a store/pop into rcvr"
												ext = (extension+32)]])
		and: [scanner thirdByte = offset]]