filePositionFromSourcePointer: anInteger
	"Return the position of the source chunk addressed by anInteger"
	"This implements the recent 32M source file algorithm"

	| hi lo |
	hi _ anInteger // 16r1000000.
	lo _ anInteger \\ 16r1000000.
	^hi < 3
		ifTrue: [lo]
		ifFalse: [lo + 16r1000000]