addContentsTogglesTo: aMenu 
	"Add updating menu toggles governing contents to aMenu."
	self contentsSymbolQuints
		do: [:aQuint | aQuint == #-
				ifTrue: [aMenu addLine]
				ifFalse: [aMenu
						addUpdating: aQuint third
						target: self
						action: aQuint second.
					aMenu balloonTextForLastItem: aQuint fifth]]