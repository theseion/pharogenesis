recreateSpecialObjectsArray
	"Smalltalk recreateSpecialObjectsArray"
	"The Special Objects Array is an array of object pointers used
	by the
	Squeak virtual machine. Its contents are critical and
	unchecked, so don't even think of playing here unless you
	know what you are doing."
	| newArray |
	newArray := Array new: 50.
	"Nil false and true get used throughout the interpreter"
	newArray at: 1 put: nil.
	newArray at: 2 put: false.
	newArray at: 3 put: true.
	"This association holds the active process (a ProcessScheduler)"
	newArray at: 4 put: (self associationAt: #Processor).
	"Numerous classes below used for type checking and instantiation"
	newArray at: 5 put: Bitmap.
	newArray at: 6 put: SmallInteger.
	newArray at: 7 put: ByteString.
	newArray at: 8 put: Array.
	newArray at: 9 put: Smalltalk.
	newArray at: 10 put: Float.
	newArray at: 11 put: MethodContext.
	newArray at: 12 put: BlockContext.
	newArray at: 13 put: Point.
	newArray at: 14 put: LargePositiveInteger.
	newArray at: 15 put: Display.
	newArray at: 16 put: Message.
	newArray at: 17 put: CompiledMethod.
	newArray at: 18 put: (self specialObjectsArray at: 18).
	"(low space Semaphore)"
	newArray at: 19 put: Semaphore.
	newArray at: 20 put: Character.
	newArray at: 21 put: #doesNotUnderstand:.
	newArray at: 22 put: #cannotReturn:.
	newArray at: 23 put: nil.
	"An array of the 32 selectors that are compiled as special bytecodes,
	 paired alternately with the number of arguments each takes."
	newArray at: 24 put: #(	#+ 1 #- 1 #< 1 #> 1 #<= 1 #>= 1 #= 1 #~= 1
							#* 1 #/ 1 #\\ 1 #@ 1 #bitShift: 1 #// 1 #bitAnd: 1 #bitOr: 1
							#at: 1 #at:put: 2 #size 0 #next 0 #nextPut: 1 #atEnd 0 #== 1 #class 0
							#blockCopy: 1 #value 0 #value: 1 #do: 1 #new 0 #new: 1 #x 0 #y 0 ).
	"An array of the 255 Characters in ascii order."
	newArray at: 25 put: ((0 to: 255) collect: [:ascii | Character value: ascii]).
	newArray at: 26 put: #mustBeBoolean.
	newArray at: 27 put: ByteArray.
	newArray at: 28 put: Process.
	"An array of up to 31 classes whose instances will have compact headers"
	newArray at: 29 put: self compactClassesArray.
	newArray at: 30 put: (self specialObjectsArray at: 30).
	"(delay Semaphore)"
	newArray at: 31 put: (self specialObjectsArray at: 31).
	"(user interrupt Semaphore)"
	"Prototype instances that can be copied for fast initialization"
	newArray at: 32 put: (Float new: 2).
	newArray at: 33 put: (LargePositiveInteger new: 4).
	newArray at: 34 put: Point new.
	newArray at: 35 put: #cannotInterpret:.
	"Note: This must be fixed once we start using context prototypes (yeah, right)"
	"(MethodContext new: CompiledMethod fullFrameSize)."
	newArray at: 36 put: (self specialObjectsArray at: 36). "Is the prototype MethodContext (unused by the VM)"
	newArray at: 37 put: BlockClosure.
	"(BlockContext new: CompiledMethod fullFrameSize)."
	newArray at: 38 put: (self specialObjectsArray at: 38). "Is the prototype BlockContext (unused by the VM)"
	newArray at: 39 put: (self specialObjectsArray at: 39).	"preserve external semaphores"
	"array of objects referred to by external code"
	newArray at: 40 put: PseudoContext.
	"newArray at: 41 put: TranslatedMethod."
	"finalization Semaphore"
	newArray at: 42 put: ((self specialObjectsArray at: 42) ifNil: [Semaphore new]).
	newArray at: 43 put: LargeNegativeInteger.
	"External objects for callout.
	 Note: Written so that one can actually completely remove the FFI."
	newArray at: 44 put: (self at: #ExternalAddress ifAbsent: []).
	newArray at: 45 put: (self at: #ExternalStructure ifAbsent: []).
	newArray at: 46 put: (self at: #ExternalData ifAbsent: []).
	newArray at: 47 put: (self at: #ExternalFunction ifAbsent: []).
	newArray at: 48 put: (self at: #ExternalLibrary ifAbsent: []).
	newArray at: 49 put: #aboutToReturn:through:.
	newArray at: 50 put: #run:with:in:.
	"Now replace the interpreter's reference in one atomic operation"
	self specialObjectsArray become: newArray