idctBlockInt: anArray component: aColorComponent

	|  qt ws anACTerm dcval z1 z2 z3 z4 z5 t0 t1 t2 t3 t10 t11 t12 t13 shift |
	qt _ self qTable at: (aColorComponent qTableIndex).
	ws _ Array new: DCTSize2.

	"Pass 1: process columns from anArray, store into work array"
	shift _ 1 bitShift: ConstBits - Pass1Bits.
	1 to: DCTSize do:
		[:i |
		anACTerm _ (1 to: DCTSize-1)
			detect: [:row | (anArray at: (row * DCTSize + i)) ~= 0]
			ifNone: [nil].
		anACTerm isNil
			ifTrue:
				[dcval _ (anArray at: i) * (qt at: 1) bitShift: Pass1Bits.
				(0 to: DCTSize-1) do: [:j | ws at: (j * DCTSize + i) put: dcval]]
			ifFalse:
				[z2 _ (anArray at: (DCTSize * 2 + i)) * (qt at: (DCTSize * 2 + i)).
				z3 _ (anArray at: (DCTSize * 6 + i)) * (qt at: (DCTSize * 6 + i)).
				z1 _ (z2 + z3) * FIXn0n541196100.
				t2 _ z1 + (z3 * FIXn1n847759065 negated).
				t3 _ z1 + (z2 * FIXn0n765366865).
				z2 _ (anArray at: i) * (qt at: i).
				z3 _ (anArray at: (DCTSize * 4 + i)) * (qt at: (DCTSize * 4 + i)).
				t0 _ (z2 + z3) bitShift: ConstBits.
				t1 _ (z2 - z3) bitShift: ConstBits.
				t10 _ t0 + t3.
				t13 _ t0 - t3.
				t11 _ t1 + t2.
				t12 _ t1 - t2.
				t0 _ (anArray at: (DCTSize * 7 + i)) * (qt at: (DCTSize * 7 + i)).
				t1 _ (anArray at: (DCTSize * 5 + i)) * (qt at: (DCTSize * 5 + i)).
				t2 _ (anArray at: (DCTSize * 3 + i)) * (qt at: (DCTSize * 3 + i)).
				t3 _ (anArray at: (DCTSize + i)) * (qt at: (DCTSize + i)).
				z1 _ t0 + t3.
				z2 _ t1 + t2.
				z3 _ t0 + t2.
				z4 _ t1 + t3.
				z5 _ (z3 + z4) * FIXn1n175875602.
				t0 _ t0 * FIXn0n298631336.
				t1 _ t1 * FIXn2n053119869.
				t2 _ t2 * FIXn3n072711026.
				t3 _ t3 * FIXn1n501321110.
				z1 _ z1 * FIXn0n899976223 negated.
				z2 _ z2 * FIXn2n562915447 negated.
				z3 _ z3 * FIXn1n961570560 negated.
				z4 _ z4 * FIXn0n390180644 negated.
				z3 _ z3 + z5.
				z4 _ z4 + z5.
				t0 _ t0 + z1 + z3.
				t1 _ t1 +z2 +z4.
				t2 _ t2 + z2 + z3.
				t3 _ t3 + z1 + z4.
				ws at: i put: (t10 + t3) >> (ConstBits - Pass1Bits).
				ws at: (DCTSize * 7 + i) put: (t10 - t3) // shift.
				ws at: (DCTSize * 1 + i) put: (t11 + t2) // shift.
				ws at: (DCTSize * 6 + i) put: (t11 - t2) // shift.
				ws at: (DCTSize * 2 + i) put: (t12 + t1) // shift.
				ws at: (DCTSize * 5 + i) put: (t12 - t1) // shift.
				ws at: (DCTSize * 3 + i) put: (t13 + t0) // shift.
				ws at: (DCTSize * 4 + i) put: (t13 - t0) // shift]].

	"Pass 2: process rows from work array, store back into anArray"
	shift _ 1 bitShift: ConstBits + Pass1Bits + 3.
	(0 to: DCTSize2-DCTSize by: DCTSize) do:
		[:i |
		z2 _ ws at: i + 3.
		z3 _ ws at: i + 7.
		z1 _ (z2 + z3) * FIXn0n541196100.
		t2 _ z1 + (z3 * FIXn1n847759065 negated).
		t3 _ z1 + (z2 * FIXn0n765366865).
		t0 _ (ws at: (i + 1)) + (ws at: (i + 5)) bitShift: ConstBits.
		t1 _ (ws at: (i + 1)) - (ws at: (i + 5)) bitShift: ConstBits.
		t10 _ t0 + t3.
		t13 _ t0 - t3.
		t11 _ t1 + t2.
		t12 _ t1 -t2.
		t0 _ ws at: (i + 8).
		t1 _ ws at: (i + 6).
		t2 _ ws at: (i + 4).
		t3 _ ws at: (i + 2).
		z1 _ t0 + t3.
		z2 _ t1 + t2.
		z3 _ t0 + t2.
		z4 _ t1 + t3.
		z5 _ (z3 + z4) * FIXn1n175875602.
		t0 _ t0 * FIXn0n298631336.
		t1 _ t1 * FIXn2n053119869.
		t2 _ t2 * FIXn3n072711026.
		t3 _ t3 * FIXn1n501321110.
		z1 _ z1 * FIXn0n899976223 negated.
		z2 _ z2 * FIXn2n562915447 negated.
		z3 _ z3 * FIXn1n961570560 negated.
		z4 _ z4 * FIXn0n390180644 negated.
		z3 _ z3 + z5.
		z4 _ z4 + z5.
		t0 _ t0 + z1 + z3.
		t1 _ t1 + z2 + z4.
		t2 _ t2 + z2 + z3.
		t3 _ t3 + z1 + z4.
		anArray at: (i + 1) put: (self sampleRangeLimit: (t10 + t3) // shift + SampleOffset).
		anArray at: (i + 8) put: (self sampleRangeLimit: (t10 - t3) // shift + SampleOffset).
		anArray at: (i + 2) put: (self sampleRangeLimit: (t11 + t2) // shift + SampleOffset).
		anArray at: (i + 7) put: (self sampleRangeLimit: (t11 - t2) // shift + SampleOffset).
		anArray at: (i + 3) put: (self sampleRangeLimit: (t12 + t1) // shift + SampleOffset).
		anArray at: (i + 6) put: (self sampleRangeLimit: (t12 - t1) // shift + SampleOffset).
		anArray at: (i + 4) put: (self sampleRangeLimit: (t13 + t0) // shift + SampleOffset).
		anArray at: (i + 5) put: (self sampleRangeLimit: (t13 - t0) // shift + SampleOffset)].


