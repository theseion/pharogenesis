garbageCollect
	"Primitive. Reclaims all garbage and answers the number of bytes of available space."
	Object flushDependents.
	Object flushEvents.
	^self primitiveGarbageCollect