popTo: aContext value: aValue
	"Replace the suspendedContext with aContext, releasing all contexts 
	between the currently suspendedContext and it."

	| callee |
	self == Processor activeProcess
		ifTrue: [^ self error: 'The active process cannot pop contexts'].
	callee := (self calleeOf: aContext) ifNil: [^ self].  "aContext is on top"
	self return: callee value: aValue