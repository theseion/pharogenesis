writeToFileWithSymbols
	| symbols nonSymbols pound |

	state = #extracted ifFalse: [self error: 'wrong state'].
	segmentName ifNil: [
		segmentName _ (FileDirectory localNameFor: fileName) sansPeriodSuffix].
		"OK that still has number on end.  This is an unusual case"
	fileName _ self class uniqueFileNameFor: segmentName.
	symbols _ OrderedCollection new.
	nonSymbols _ OrderedCollection new.
	pound _ '#' asSymbol.
	outPointers do:
		[:s | 
		((s isMemberOf: Symbol) and: [s isLiteral and: [s ~~ pound]])
			ifTrue: [symbols addLast: s]
			ifFalse: [symbols addLast: pound.  nonSymbols addLast: s]].
	(self class segmentDirectory newFileNamed: fileName)
		store: symbols asArray; cr;
		nextPutAll: segment; close.
	outPointers _ nonSymbols asArray.
	state _ #onFileWithSymbols