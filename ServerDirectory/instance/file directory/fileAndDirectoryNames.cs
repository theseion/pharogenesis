fileAndDirectoryNames
	"FileDirectory default fileAndDirectoryNames"

	^ self entries collect: [:entry | entry first]
