testInjectIntoDecompilations
	"Test various compilations decompile to the same code for a method sufficiently
	 simple that this is possible and sufficiently complex that the code generated
	 varies between the compilations."
	"self new testInjectIntoDecompilations"
	| source |
	source := (Collection sourceCodeAt: #inject:into:) asString.
	{ Encoder.
	   EncoderForV3. EncoderForLongFormV3.
	   EncoderForV3PlusClosures. EncoderForLongFormV3PlusClosures } do:
		[:encoderClass| | method |
		method := (Parser new
							encoderClass: encoderClass;
							parse: source
							class: Collection)
						generate: #(0 0 0 0).
		self assert: (Scanner new scanTokens: method decompileString)
					= #(inject: t1 into: t2
							| t3 |
							t3 ':=' t1 .
							self do: [ ':t4' | t3 ':=' t2 value: t3 value: t4 ] .
							^ t3)]