existPackageNamed: aString
	"
	self existPackageNamed: 'PackageInfo'
	self existPackageNamed: 'Zork'
	"
	^ (self allPackages anySatisfy: [:each | each packageName = aString])
			