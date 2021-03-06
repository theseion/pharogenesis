testValidZipCrc
	"See that a correct CRC does not raise an error and that we can read what we wrote."
	| reader writer bytes readBytes |
	writer := ZipWriteStream on: String new.
	writer nextPutAll: 'Hello World'.
	writer close.

	bytes := writer encodedStream contents.

	reader := ZipReadStream on: bytes.
	reader expectedCrc: writer crc.
	self shouldnt:[ readBytes := reader upToEnd] raise: CRCError.
	self assert: readBytes = 'Hello World'.

	reader := ZipReadStream on: bytes.
	reader expectedCrc: writer crc.
	self shouldnt:[ readBytes := reader contents] raise: CRCError.
	self assert: readBytes = 'Hello World'.

	reader := ZipReadStream on: bytes.
	reader expectedCrc: writer crc.
	self shouldnt:[ readBytes := reader next: 11 ] raise: CRCError.
	self assert: readBytes = 'Hello World'.
	
	reader := ZipReadStream on: bytes.
	reader expectedCrc: writer crc.
	self shouldnt:[ readBytes := reader next: 100 ] raise: CRCError.
	self assert: readBytes = 'Hello World'.