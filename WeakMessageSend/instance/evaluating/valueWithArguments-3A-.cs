valueWithArguments: anArray
	self ensureReceiverAndArguments ifFalse: [ ^nil ].
	^ self receiver 
		perform: selector 
		withArguments: (self collectArguments: anArray)