saveOnFile
	"Ask the user for a filename and save myself on a SmartReferenceStream file.  Writes out the version and class structure.  The file is fileIn-able.  UniClasses will be filed out."

	| aFileName fileStream ok |
	aFileName _ ('my {1}' translated format: {self class name}) asFileName.	"do better?"
	aFileName _ FillInTheBlank request: 'File name? (".morph" will be added to end)' translated 
			initialAnswer: aFileName.
	aFileName isEmpty ifTrue: [^ Beeper beep].
	self allMorphsDo: [:m | m prepareToBeSaved].

	ok _ aFileName endsWith: '.morph'.	"don't double them"
	ok _ ok | (aFileName endsWith: '.sp').
	ok ifFalse: [aFileName _ aFileName,'.morph'].
	fileStream _ FileStream newFileNamed: aFileName asFileName.
	fileStream fileOutClass: nil andObject: self.	"Puts UniClass definitions out anyway"