serviceImageImports
	"Answer a service for reading a graphic into ImageImports"

	^	SimpleServiceEntry
			provider: self 
			label: 'read graphic into ImageImports'
			selector: #importImage:
			description: 'Load a graphic, placing it in the ImageImports repository.'
			buttonLabel: 'import'