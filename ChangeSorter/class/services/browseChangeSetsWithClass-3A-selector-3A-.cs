browseChangeSetsWithClass: class selector: selector
	"Put up a menu comprising a list of change sets that hold changes for the given class and selector.  If the user selects one, open a single change-sorter onto it"

	| hits index |
	hits _ self allChangeSets select: 
		[:cs | (cs atSelector: selector class: class) ~~ #none].
	hits isEmpty ifTrue: [^ self inform: class name, '.', selector , '
is not in any change set'].
	index _ hits size == 1
		ifTrue:	[1]
		ifFalse:	[(PopUpMenu labelArray: (hits collect: [:cs | cs name])
					lines: #()) startUp].
	index = 0 ifTrue: [^ self].
	(ChangeSorter new myChangeSet: (hits at: index)) open.
