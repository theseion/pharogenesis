methods
	"Answer a ClassCategoryReader for compiling messages that are not classified, as in fileouts made with Smalltalk/V"

	^ ClassCategoryReader new setClass: self
							category: 'as yet unclassified' asSymbol