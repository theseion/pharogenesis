testExample1

	"This is the first example from the specification document (FIPS PUB 180-1)"

	hash := SecureHashAlgorithm new hashMessage: 'abc'.
	self assert: (hash = 16rA9993E364706816ABA3E25717850C26C9CD0D89D).
		