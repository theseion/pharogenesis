isFlapIDEnabled:  aFlapID
	"Answer whether a flap of the given ID is enabled in this project"

	| disabledFlapIDs  |
	disabledFlapIDs _ self parameterAt: #disabledGlobalFlapIDs ifAbsent: [^ true].
	^ (disabledFlapIDs includes: aFlapID) not