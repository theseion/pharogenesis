copyFromByteArray: byteArray 
	"This method should work with either byte orderings"

	| myHack byteHack |
	myHack := Form new hackBits: self.
	byteHack := Form new hackBits: byteArray.
	SmalltalkImage current  isLittleEndian ifTrue: [byteHack swapEndianness].
	byteHack displayOn: myHack