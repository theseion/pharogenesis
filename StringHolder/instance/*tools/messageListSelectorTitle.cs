messageListSelectorTitle
	| selector aString aStamp aSize |

	(selector := self selectedMessageName) ifNil: [
			aSize := self messageList size.
			^ (aSize == 0 ifTrue: ['no'] ifFalse: [aSize printString]), ' message', (aSize == 1 ifTrue: [''] ifFalse: ['s'])] ifNotNil: [
			aString := selector truncateWithElipsisTo: 28.
			^ (aStamp := self timeStamp) size > 0
				ifTrue: [aString, String cr, aStamp]
				ifFalse: [aString]]