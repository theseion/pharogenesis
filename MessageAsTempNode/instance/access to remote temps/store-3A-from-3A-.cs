store: expr from: encoder 
	"ctxt tempAt: n -> ctxt tempAt: n put: expr (see Assignment).
	For assigning into temps of a context being debugged."

	selector key ~= #tempAt: 
		ifTrue: [^self error: 'cant transform this message'].
	^ MessageAsTempNode new
		receiver: receiver
		selector: #tempAt:put:
		arguments: (arguments copyWith: expr)
		precedence: precedence
		from: encoder