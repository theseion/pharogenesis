fullCopy
	"Deprecated, but maintained for backward compatibility with existing code (no senders in the base 3.0 image).   Calls are revectored to #veryDeepCopy, but note that #veryDeepCopy does not do exactly the same thing that the original #fullCopy did, so beware!"

	^ self veryDeepCopy