addDebuggingItemsTo: aMenu hand: aHandMorph
	aMenu add: 'debug...' translated subMenu:  (self buildDebugMenu: aHandMorph)