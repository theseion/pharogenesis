httpGetDocument: url args: args accept: mimeType request: requestString
	"Return the exact contents of a web object. Asks for the given MIME 
type. If mimeType is nil, use 'text/html'. An extra requestString may be 
submitted and must end with crlf.  The parsed header is saved. Use a 
proxy server if one has been registered.  tk 7/23/97 17:12"
	"Note: To fetch raw data, you can use the MIME type 
'application/octet-stream'."

	| serverName serverAddr port sock header length bare page list firstData 
aStream index connectToHost connectToPort type newUrl |
	Socket initializeNetwork.
	bare _ (url asLowercase beginsWith: 'http://') 
		ifTrue: [url copyFrom: 8 to: url size]
		ifFalse: [url].
	bare _ bare copyUpTo: $#.  "remove fragment, if specified"
	serverName _ bare copyUpTo: $/.
	page _ bare copyFrom: serverName size + 1 to: bare size.
	(serverName includes: $:) 
		ifTrue: [ index _ serverName indexOf: $:.
			port _ (serverName copyFrom: index+1 to: serverName size) asNumber.
			serverName _ serverName copyFrom: 1 to: index-1. ]
		ifFalse: [ port _ self defaultPort ].
	page size = 0 ifTrue: [page _ '/'].
	"add arguments"
	args ifNotNil: [page _ page, (self argString: args) ].


	HTTPProxyServer isNil
		ifTrue: [ 
			connectToHost _ serverName.
			connectToPort _ port ]
		ifFalse:  [
			page _ 'http://', serverName, ':', port printString, page.		"put back 
together"
			connectToHost _ HTTPProxyServer.
			connectToPort _ HTTPProxyPort].
	
	self flag: #XXX.  "this doesn't make sense if a user isn't available for 
questioning...  -ls"
	self retry: [serverAddr _ NetNameResolver addressForName: connectToHost 
timeout: 20.
				serverAddr ~~ nil] 
		asking: 'Trouble resolving server name.  Keep trying?'
		ifGiveUp: [Socket deadServer: connectToHost.
				^ 'Could not resolve the server named: ', connectToHost].

3 timesRepeat: [
	sock _ HTTPSocket new.
	sock connectTo: serverAddr port: connectToPort.
	(sock waitForConnectionUntil: (self deadlineSecs: 30)) ifFalse: [
		Socket deadServer: connectToHost.  sock destroy.
		^ 'Server ',connectToHost,' is not responding'].
	Transcript cr; cr; show: url; cr.
	sock sendCommand: 'GET ', page, ' HTTP/1.0', CrLf, 
		(mimeType ifNotNil: ['ACCEPT: ', mimeType, CrLf] ifNil: ['']),
		'ACCEPT: text/html', CrLf,	"Always accept plain text"
		HTTPBlabEmail,	"may be empty"
		requestString,	"extra user request. Authorization"
		'User-Agent: Squeak 1.31', CrLf,
		'Host: ', serverName, ':', port printString, CrLf.	"blank line 
automatically added"

	list _ sock getResponseUpTo: CrLf, CrLf ignoring: (String with: CR).	"list = header, CrLf, CrLf, 
beginningOfData"
	header _ list at: 1.
	"Transcript show: page; cr; show: header; cr."
	firstData _ list at: 3.
	header isEmpty 
		ifTrue: [aStream _ 'server aborted early']
		ifFalse: [
			"dig out some headers"
			sock header: header.
			length _ sock getHeader: 'content-length'.
			length ifNotNil: [ length _ length asNumber ].
			type _ sock getHeader: 'content-type'.
			sock responseCode first = $3 ifTrue: [
				newUrl _ sock getHeader: 'location'.
				newUrl ifNotNil: [ 
					Transcript show: 'redirecting to ', newUrl; cr.
					sock destroy.
					^self httpGetDocument: newUrl  args: args  accept: mimeType ] ].
			aStream _ sock getRestOfBuffer: firstData totalLength: length.
			sock responseCode = '401' ifTrue: [^ header, aStream contents].
			].
	sock destroy.	"Always OK to destroy!"
	aStream class ~~ String ifTrue: [
 		^ MIMEDocument contentType: type content: aStream contents url: url].
	aStream = 'server aborted early' ifFalse: [
		]
	].