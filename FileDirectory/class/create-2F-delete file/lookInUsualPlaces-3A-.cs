lookInUsualPlaces: fileName
	"Check the default directory, the imagePath, and the vmPath (and the vmPath's owner) for this file."

	| vmp |
	(FileDirectory default fileExists: fileName)
		ifTrue: [^ FileDirectory default fileNamed: fileName].

	((vmp := FileDirectory on: SmalltalkImage current imagePath) fileExists: fileName)
		ifTrue: [^ vmp fileNamed: fileName].

	((vmp := FileDirectory on: SmalltalkImage current vmPath) fileExists: fileName)
		ifTrue: [^ vmp fileNamed: fileName].

	((vmp := vmp containingDirectory) fileExists: fileName)
		ifTrue: [^ vmp fileNamed: fileName].

	^ nil