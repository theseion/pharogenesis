rename: aString 
	"The new name of the receiver is the argument, aString."

	| newName |
	(newName := aString asSymbol) ~= self name
		ifFalse: [^ self].
	(self environment includesKey: newName)
		ifTrue: [^ self error: newName , ' already exists'].
	(Undeclared includesKey: newName)
		ifTrue: [self inform: 'There are references to, ' , aString printString , '
from Undeclared. Check them after this change.'].
	self environment renameClass: self as: newName.
	name := newName