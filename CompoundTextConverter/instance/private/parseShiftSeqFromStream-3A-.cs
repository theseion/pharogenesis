parseShiftSeqFromStream: aStream

	| c set target id |
	c := aStream basicNext.
	c = $$ ifTrue: [
		set := #multibyte.
		c := aStream basicNext.
		c = $( ifTrue: [target := 1].
		c = $) ifTrue: [target := 2].
		target ifNil: [target := 1. id := c]
			ifNotNil: [id := aStream basicNext].
	] ifFalse: [
		c = $( ifTrue: [target := 1. set := #nintyfour].
		c = $) ifTrue: [target := 2. set := #nintyfour].
		c = $- ifTrue: [target := 2. set := #nintysix].
		id := aStream basicNext.
	].
	(set = #multibyte and: [id = $B]) ifTrue: [
		state charSize: 2.
		target = 1 ifTrue: [
			state g0Size: 2.
			state g0Leading: 1.
		] ifFalse: [
			state g1Size: 2.
			state g1Leading: 1.
		].
		^ self
	].
	(set = #multibyte and: [id = $A]) ifTrue: [
		state charSize: 2.
		target = 1 ifTrue: [
			state g0Size: 2.
			state g0Leading: 2.
		] ifFalse: [
			state g1Size: 2.
			state g1Leading: 2.
		].
		^ self
	].

	(set = #nintyfour and: [id = $B or: [id = $J]]) ifTrue: [
		state charSize: 1.
		state g0Size: 1.
		state g0Leading: 0.
		^ self
	].
	(set = #nintysix and: [id = $A]) ifTrue: [
		state charSize: 1.
		state g1Size: 1.
		state g1Leading: 0.
		^ self
	].