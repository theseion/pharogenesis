testIncludesAssociationNoValue

	| association dictionary |
	
	association := Association key: #key.
	
	self assert: association value isNil.
	
	dictionary := Dictionary new.
	
	dictionary add: association.
	
	self assert: (dictionary at: #key) isNil

	
	
	