text: aTextItem handler: aHandlerBlock
	| resolvedClassInstance |
	
	resolvedClassInstance := self defaultMenuItem.
	resolvedClassInstance text: aTextItem.
	resolvedClassInstance handler: aHandlerBlock.
	^resolvedClassInstance