serviceBrowseDotChangesFile
	"Answer a service for opening a changelist browser on the tail end of a .changes file"

	^ SimpleServiceEntry 
		provider: self 
		label: 'recent changes in file'
		selector: #browseRecentLogOnPath:
		description: 'open a changelist tool on recent changes in file'
		buttonLabel: 'recent changes'