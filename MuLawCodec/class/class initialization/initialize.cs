initialize
	"Build the 256 entry table to be used to decode 8-bit uLaw-encoded samples."
	"MuLawCodec initialize"

	| encoded codec lastEncodedPos lastEncodedNeg |
	DecodingTable _ Array new: 256.
	codec _ self new.
	lastEncodedPos _ nil.
	lastEncodedNeg _ nil.
	4095 to: 0 by: -1 do: [:s |
		encoded _ codec uLawEncode12Bits: s.
		lastEncodedPos = encoded
			ifFalse: [
				DecodingTable at: (encoded + 1) put: (s bitShift: 3).
				lastEncodedPos _ encoded].
		encoded _ encoded bitOr: 16r80.
		lastEncodedNeg = encoded
			ifFalse: [
				DecodingTable at: (encoded + 1) put: (s bitShift: 3) negated.
				lastEncodedNeg _ encoded]].
