setDefaultParameters
	"change the receiver's appareance parameters"
	| colorFromMenu worldColor menuColor menuBorderColor |
	colorFromMenu := Preferences menuColorFromWorld
				and: [Display depth > 4]
				and: [(worldColor := self currentWorld color) isColor].
	menuColor := colorFromMenu
				ifTrue: [worldColor luminance > 0.7
						ifTrue: [worldColor mixed: 0.85 with: Color black]
						ifFalse: [worldColor mixed: 0.4 with: Color white]]
				ifFalse: [Preferences menuColor].
	menuBorderColor := Preferences menuAppearance3d
				ifTrue: [#raised]
				ifFalse: [colorFromMenu
						ifTrue: [worldColor muchDarker]
						ifFalse: [Preferences menuBorderColor]].
					Preferences roundedMenuCorners
		ifTrue: [self useRoundedCorners].
		
	self
		setColor: menuColor
		borderWidth: Preferences menuBorderWidth
		borderColor: menuBorderColor.
	self
		updateColor: self
		color: self color
		intensity: 1.