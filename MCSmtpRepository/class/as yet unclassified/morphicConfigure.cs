morphicConfigure
	| address |
	address := UIManager default request: 'Email address:' translated.
	^ address isEmpty ifFalse: [self new emailAddress: address]