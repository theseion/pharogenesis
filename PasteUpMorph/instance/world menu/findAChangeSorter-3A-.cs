findAChangeSorter: evt
	"Locate a change sorter, open it, and bring it to the front.  Create one if necessary"

	self findAWindowSatisfying:
		[:aWindow | (aWindow model isMemberOf: ChangeSorter) or:
				[aWindow model isKindOf: DualChangeSorter]] orMakeOneUsing: [DualChangeSorter new morphicWindow]