defaultMIMEdatabase
	| d |
	(d _ Dictionary new)
	at: 'html' put: 'text/html';
	at: 'htm' put: 'text/html';
	at: 'xml' put: 'text/xml';
	at: 'txt' put: 'text/plain';
	at: 'c' put: 'text/plain';
	at: 'gif' put: 'image/gif';
	at: 'jpg' put: 'image/jpeg';
	at: 'jpeg' put: 'image/jpeg';
	at: 'xbm' put: 'image/x-xbitmap';
	at: 'mid' put: 'audio/midi';
	at: 'doc' put: 'application/ms-word-document'.
	^d