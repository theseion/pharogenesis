fileOut
	"File out the receiver, to a file whose name is a function of the  
	change-set name and a unique numeric tag."
	| slips nameToUse internalStream |
	self checkForConversionMethods.
	ChangeSet promptForDefaultChangeSetDirectoryIfNecessary.
	nameToUse := self defaultChangeSetDirectory 
			nextNameFor: self name extension: FileStream cs.
	nameToUse := self defaultChangeSetDirectory fullNameFor: nameToUse.
	Cursor write showWhile: [
			internalStream := (String new: 10000) writeStream.
			internalStream header; timeStamp.
			self fileOutPreambleOn: internalStream.
			self fileOutOn: internalStream.
			self fileOutPostscriptOn: internalStream.
			internalStream trailer.

			FileStream writeSourceCodeFrom: internalStream baseName: (nameToUse copyFrom: 1 to: nameToUse size - 3) isSt: false.
	].
	Preferences checkForSlips
		ifFalse: [^ self].
	slips := self checkForSlips.
	(slips size > 0
			and: [self confirm: 'Methods in this fileOut have halts
or references to the Transcript
or other ''slips'' in them.
Would you like to browse them?' translated])
		ifTrue: [self systemNavigation browseMessageList: slips name: 'Possible slips in ' , name]