chooseUpdateList
	"When there is more than one set of update servers, let the user choose which we will update from.  Put it at the front of the list. Return false if the user aborted.  If the preference #promptForUpdateServer is false, then suppress that prompt, in effect using the same server choice that was used the previous time (a convenience for those of us who always answer the same thing to the prompt.)"

	| index him |
	((UpdateUrlLists size > 1) and: [Preferences promptForUpdateServer])
		ifTrue:
			[index := UIManager default 
				chooseFrom: (UpdateUrlLists collect: [:each | each first]) 
				lines: #()
				title: 'Choose a group of servers\from which to fetch updates.' translated withCRs.
			index > 0 ifTrue:
				[him := UpdateUrlLists at: index.
				UpdateUrlLists removeAt: index.
				UpdateUrlLists addFirst: him].
			^ index > 0].
	^ true