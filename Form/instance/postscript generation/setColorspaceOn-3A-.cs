setColorspaceOn:aStream
	self numComponents = 1 ifTrue:[aStream print:'/DeviceGray setcolorspace 0 setgray'; cr.]
		ifFalse:[aStream print:'/DeviceRGB setcolorspace'; cr.].