veryDeepInner: deepCopier
	"Copy all of my instance variables.  Some need to be not copied at all, but shared."

	super veryDeepInner: deepCopier.
	format := format veryDeepCopyWith: deepCopier.
	target := target.					"Weakly copied"
	lastValue := lastValue veryDeepCopyWith: deepCopier.
	getSelector := getSelector.			"Symbol"
	putSelector := putSelector.		"Symbol"
	floatPrecision := floatPrecision veryDeepCopyWith: deepCopier.
	growable := growable veryDeepCopyWith: deepCopier.
	stepTime := stepTime veryDeepCopyWith: deepCopier.
	autoAcceptOnFocusLoss := autoAcceptOnFocusLoss veryDeepCopyWith: deepCopier.
	minimumWidth := minimumWidth veryDeepCopyWith: deepCopier.
	maximumWidth := maximumWidth veryDeepCopyWith: deepCopier.
