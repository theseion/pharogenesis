openMenu

"temporary support of WorldWindow (easy open)"


	"Build the open window menu for the world."
	| menu |
	menu _ (MenuMorph entitled: 'open...') defaultTarget: self.
	menu addStayUpItem.
	menu add: 'browser' action: #openBrowser.
	menu add: 'package browser' action: #openPackageBrowser.
	menu add: 'method finder' action: #openSelectorBrowser.
	menu add: 'workspace' action: #openWorkspace.
	menu add: 'file list' action: #openFileList.
	menu add: 'file...' action: #openFileDirectly.
	menu add: 'transcript' action: #openTranscript.
	menu add: 'inner world' action: #openWorldInWindow.
	menu addLine.
	menu add: 'simple change sorter' selector: #openChangeSorter: argument: 1.
	menu add: 'dual change sorter' selector: #openChangeSorter: argument: 2.
	menu addLine.
	menu add: 'email reader' action: #openEmail.
	menu add: 'web browser' action: #openWebBrowser.
	menu add: 'IRC chat' action: #openIRC.
	menu addLine.
	(Preferences mvcProjectsAllowed and: [Smalltalk includesKey: #StandardSystemView])
		ifTrue: [menu add: 'mvc project' action: #openMVCProject].
	menu add: 'morphic project' action: #openMorphicProject.
	^ menu
