fileNamed: fileName 
	^ self concreteStream fileNamed: (self fullName: fileName)