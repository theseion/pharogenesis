mostSpecificPackageOfMethod: aMethodReference
	^ self mostSpecificPackageOfMethod: aMethodReference ifNone: [self noPackageFound]