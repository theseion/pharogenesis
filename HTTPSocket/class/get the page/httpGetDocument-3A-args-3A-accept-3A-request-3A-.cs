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
	bare := (url asLowercase beginsWith: 'http://') 
		ifTrue: [url copyFrom: 8 to: url size]
		ifFalse: [url].
	bare := bare copyUpTo: $#.  "remove fragment, if specified"
	serverName := bare copyUpTo: $/.
	page := bare copyFrom: serverName size + 1 to: bare size.
	(serverName includes: $:) 
		ifTrue: [ index := serverName indexOf: $:.
			port := (serverName copyFrom: index+1 to: serverName size) asNumber.
			serverName := serverName copyFrom: 1 to: index-1. ]
		ifFalse: [ port := self defaultPort ].
	page size = 0 ifTrue: [page := '/'].
	"add arguments"
	args ifNotNil: [page := page, (self argString: args) ].


	(self shouldUseProxy: serverName)
		ifFalse: [ 
			connectToHost := serverName.
			connectToPort := port ]
		ifTrue:  [
			page := 'http://', serverName, ':', port printString, page.		"put back 
together"
			connectToHost := self httpProxyServer.
			connectToPort := self httpProxyPort].
	

	serverAddr := NetNameResolver addressForName: connectToHost timeout: 20.
	serverAddr ifNil: [
		^ 'Could not resolve the server named: ', connectToHost].

3 timesRepeat: [
	sock := HTTPSocket new.
	sock connectTo: serverAddr port: connectToPort.
	(sock waitForConnectionFor: 30 ifTimedOut: [false]) ifFalse: [
		Socket deadServer: connectToHost.  sock destroy.
		^ 'Server ',connectToHost,' is not responding'].
	"Transcript cr;show: url; cr.
	Transcript show: page; cr."
	sock sendCommand: 'GET ', page, ' HTTP/1.0', String crlf, 
		(mimeType ifNotNil: ['ACCEPT: ', mimeType,  String crlf] ifNil: ['']),
		'ACCEPT: text/html',  String crlf,	"Always accept plain text"
		HTTPProxyCredentials,
		HTTPBlabEmail,	"may be empty"
		requestString,	"extra user request. Authorization"
		self userAgentString,  String crlf,
		'Host: ', serverName, ':', port printString,  String crlf.	"blank line 
automatically added"

	list := sock getResponseUpTo:  String crlf,  String crlf ignoring: String cr. "list = header, CrLf, CrLf, 
beginningOfData"
	header := list at: 1.
	"Transcript show: page; cr; show: header; cr."
	firstData := list at: 3.
	header isEmpty 
		ifTrue: [aStream := 'server aborted early']
		ifFalse: [
			"dig out some headers"
			sock header: header.
			length := sock getHeader: 'content-length'.
			length ifNotNil: [ length := length asNumber ].
			type := sock getHeader: 'content-type'.
			sock responseCode first = $3 ifTrue: [
				newUrl := sock getHeader: 'location'.
				newUrl ifNotNil: [ 
					Transcript show: 'redirecting to ', newUrl; cr.
					sock destroy.
					newUrl := self expandUrl: newUrl ip: serverAddr port: connectToPort.
					^self httpGetDocument: newUrl args: args  accept: mimeType request: requestString] ].
			aStream := sock getRestOfBuffer: firstData totalLength: length.
			"a 400-series error"
			sock responseCode first = $4 ifTrue: [^ header, aStream contents].
			].
	sock destroy.	"Always OK to destroy!"
	aStream class ~~ String ifTrue: [
 		^ MIMEDocument contentType: type content: aStream contents url: url].
	aStream = 'server aborted early' ifTrue: [ ^aStream ].
	].

{'HTTPSocket class>>httpGetDocument:args:accept:request:'. aStream. url} inspect.

	^'some other bad thing happened!'