fileNameFromFormattedItem: item
	"Extract fileName and folderString from a formatted fileList item string"

	| from to |
	self sortingByName
		ifTrue: [
			from := item lastIndexOf: $( ifAbsent: [0].
			to := item lastIndexOf: $) ifAbsent: [0]]
		ifFalse: [
			from := item indexOf: $( ifAbsent: [0].
			to := item indexOf: $) ifAbsent: [0]].
	^ (from * to = 0
		ifTrue: [item]
		ifFalse: [item copyReplaceFrom: from to: to with: '']) withBlanksTrimmed