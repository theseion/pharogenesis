addAListPane: aListPane to: window at: nominalFractions plus: verticalOffset

	| row switchHeight divider |

	row _ AlignmentMorph newColumn
		hResizing: #spaceFill;
		vResizing: #spaceFill;
		layoutInset: 0;
		borderWidth: 1;
		layoutPolicy: ProportionalLayout new.
	switchHeight _ 25.
	self 
		addMorphicSwitchesTo: row 
		at: (
			LayoutFrame 
				fractions: (0@1 corner: 1@1) 
				offsets: (0@(1-switchHeight)  corner: 0@0)
		).

	divider _ BorderedSubpaneDividerMorph forTopEdge.
	Preferences alternativeWindowLook ifTrue:[
		divider extent: 4@4; color: Color transparent; borderColor: #raised; borderWidth: 2.
	].
	row 
		addMorph: divider
		fullFrame: (
			LayoutFrame 
				fractions: (0@1 corner: 1@1) 
				offsets: (0@switchHeight negated corner: 0@(1-switchHeight))
		).	

	row 
		addMorph: aListPane
		fullFrame: (
			LayoutFrame 
				fractions: (0@0 corner: 1@1) 
				offsets: (0@0 corner: 0@(switchHeight negated))
		).	

	window 
		addMorph: row
		fullFrame: (
			LayoutFrame 
				fractions: nominalFractions 
				offsets: (0@verticalOffset corner: 0@0)
		).	
	row on: #mouseEnter send: #paneTransition: to: window.
	row on: #mouseLeave send: #paneTransition: to: window.

