normalizeContents: webDocument
	(webDocument isString) ifTrue: [
		^MIMEDocument
			contentType: 'text/plain'
			content: 'error occured retrieving ', self toText, ': ', webDocument
			url: (Url absoluteFromText: '')].
	webDocument contentType = MIMEDocument defaultContentType ifTrue: [
		^MIMEDocument contentType: (MIMEDocument guessTypeFromName: self path last) 
			content: webDocument content url: webDocument url ].

	^webDocument