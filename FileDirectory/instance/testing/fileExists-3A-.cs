fileExists: filenameOrPath
	"Answer true if a file of the given name exists. The given name may be either a full path name or a local file within this directory."
	"FileDirectory default fileExists: Smalltalk sourcesName"

	| fName dir |
	FileDirectory splitName: filenameOrPath to:
		[:filePath :name |
			fName _ name.
			filePath isEmpty
				ifTrue: [dir _ self]
				ifFalse: [dir _ FileDirectory on: filePath]].
	self isCaseSensitive 
		ifTrue:[^dir fileNames includes: fName]
		ifFalse:[^dir fileNames anySatisfy: [:name| name sameAs: fName]].	