adaptToInteger: rcvr andSend: selector
	"If no method has been provided for adapting an object to a Integer,
	then it may be adequate to simply adapt it to a number."
	^ self adaptToNumber: rcvr andSend: selector