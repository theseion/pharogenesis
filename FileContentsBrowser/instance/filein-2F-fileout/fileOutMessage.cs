fileOutMessage

	self selectedMessageName ifNil: [^self].
	Cursor write showWhile: [
		self selectedClassOrMetaClass fileOutMethod: self selectedMessageName].