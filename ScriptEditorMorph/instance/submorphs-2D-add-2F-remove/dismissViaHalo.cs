dismissViaHalo
	"The user has clicked in the delete halo-handle.  This provides a hook in case some concomitant action should be taken, or if the particular morph is not one which should be put in the trash can, for example."

	self resistsRemoval ifTrue: [^ self].
	self destroyScript