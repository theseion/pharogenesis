testBecomeForwardDontCopyIdentityHash
	"Check that
		1. the argument to becomeForward: is NOT modified to have the receiver's identity hash.
		2. the receiver's identity hash is unchanged."

 	| a b hb |

	a := 'ab' copy.
	b := 'cd' copy.
	hb := b identityHash.

	a becomeForward: b copyHash: false.

	self 
		assert: a identityHash = hb;
		assert: b identityHash = hb.

