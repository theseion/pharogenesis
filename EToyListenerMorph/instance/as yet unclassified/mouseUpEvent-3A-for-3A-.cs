mouseUpEvent: event for: aMorph

	| depictedObject |

	depictedObject _ aMorph firstSubmorph valueOfProperty: #depictedObject.
	event hand attachMorph: depictedObject.
	self class removeFromGlobalIncomingQueue: depictedObject.
	self rebuild.
