isBreakableAtIndex: index

	^ (EncodedCharSet at: ((text at: index) leadingChar + 1)) isBreakableAt: index in: text.
