initialize
	"Initialize the receiver."

	super initialize.
	self
		borderStyle: (self theme plainGroupPanelBorderStyleFor: self);
		changeTableLayout; 
		layoutInset: (4@4 corner: 4@4);
		cellInset: 8;
		vResizing: #spaceFill;
		hResizing: #spaceFill