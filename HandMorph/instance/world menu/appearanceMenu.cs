appearanceMenu
	"Build the appearance menu for the world."
	| menu screenCtrl |
	screenCtrl _ ScreenController new.
	menu _ (MenuMorph entitled: 'appearance...') defaultTarget: self.
	menu addStayUpItem.
	menu add: 'window colors...' target: Preferences action: #windowSpecificationPanel.
	menu balloonTextForLastItem: 'Lets you specify colors for standard system windows.'.
	menu add: 'system fonts...' target: self action: #standardFontDo.
	menu balloonTextForLastItem: 'Choose the standard fonts to use for code, lists, menus, window titles, etc.'.
	menu add: 'text highlight color...' target: Preferences action: #chooseTextHighlightColor.
	menu balloonTextForLastItem: 'Choose which color should be used for text highlighting in Morphic.'.
	menu add: 'insertion point color...' target: Preferences action: #chooseInsertionPointColor.
	menu balloonTextForLastItem: 'Choose which color to use for the text insertion point in Morphic.'.
	menu addLine.

	menu addUpdating: #menuColorString target: Preferences action: #toggleMenuColorPolicy.
	menu balloonTextForLastItem: 'Governs whether menu colors should be derived from the desktop color.'.
	menu addUpdating: #roundedCornersString target: Preferences action: #toggleRoundedCorners.
	menu balloonTextForLastItem: 'Governs whether morphic windows and menus should have rounded corners.'.

	menu addLine.

	menu add: 'full screen on' target: screenCtrl action: #fullScreenOn.
	menu balloonTextForLastItem: 'puts you in full-screen mode, if not already there.'.
	menu add: 'full screen off' target: screenCtrl action: #fullScreenOff.
	menu balloonTextForLastItem: 'if in full-screen mode, takes you out of it.'.
	menu addLine.

	menu add: 'set display depth...' action: #setDisplayDepth.
	menu balloonTextForLastItem: 'choose how many bits per pixel.'.
	menu add: 'set desktop color...' action: #changeBackgroundColor.
	menu balloonTextForLastItem: 'choose a uniform color to use as desktop background.'.
	menu add: 'set gradient color...' target: self world action: #setGradientColor:.
	menu balloonTextForLastItem: 'choose second color to use as gradient for desktop background.'.
	menu add: 'use texture background' target: self world action: #setStandardTexture.
	menu balloonTextForLastItem: 'apply a graph-paper-like texture background to the desktop.'.
	menu addLine.
	menu add: 'clear turtle trails from desktop' target: self world action: #clearTurtleTrails.
	menu balloonTextForLastItem: 'remove any pigment laid down on the desktop by objects moving with their pens down.'.

	^ menu