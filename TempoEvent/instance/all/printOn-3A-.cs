printOn: aStream

	aStream nextPut: $(.
	time printOn: aStream.
	aStream nextPutAll: ': '.
	((120.0 * (500000.0 / tempo)) roundTo: 0.01) printOn: aStream.
	aStream nextPut: $).
