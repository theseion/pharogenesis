putFile: fileStream named: fileNameOnServer
	"Just FTP a local fileStream to the server.  (Later -- Use a proxy server if one has been registered.)"

	client := self openFTPClient.
	client binary.
	[client putFileStreamContents: fileStream as: fileNameOnServer]
		ensure: [self quit]