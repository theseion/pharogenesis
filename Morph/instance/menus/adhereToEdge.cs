adhereToEdge
	| menu |
	menu := MenuMorph new defaultTarget: self.
	#(top right bottom left - center - topLeft topRight bottomRight bottomLeft - none)
		do: [:each |
			each == #-
				ifTrue: [menu addLine]
				ifFalse: [menu add: each asString translated selector: #setToAdhereToEdge: argument: each]].
	menu popUpEvent: self currentEvent in: self world