isObjectForwarded: oop
	"Return true if the given object has a forwarding table entry during a compaction or become operation."

	^ (oop bitAnd: 1) = 0 "(isIntegerObject: oop) not" and:
	   [ ((self longAt: oop) bitAnd: MarkBit) ~= 0 ]