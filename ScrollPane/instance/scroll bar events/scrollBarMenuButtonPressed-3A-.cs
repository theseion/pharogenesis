scrollBarMenuButtonPressed: event
	self use: menuSelector orMakeModelSelectorFor: 'MenuButtonPressed:'
		in: [:sel | menuSelector _ sel.  model perform: sel with: event]