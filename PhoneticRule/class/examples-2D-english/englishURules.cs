englishURules
	^ #((' '		'un'	'i'		'y/uw/n')
		(' '		'un'	''		'ah/n')
		(' '		'upon'	''		'ax/p/ao/n')
		('t'		'ur'		'#'		'uh/r')
		('s'		'ur'		'#'		'uh/r')
		('r'		'ur'		'#'		'uh/r')
		('d'		'ur'		'#'		'uh/r')
		('l'		'ur'		'#'		'uh/r')
		('z'		'ur'		'#'		'uh/r')
		('n'		'ur'		'#'		'uh/r')
		('j'		'ur'		'#'		'uh/r')
		('th'	'ur'		'#'		'uh/r')
		('ch'	'ur'		'#'		'uh/r')
		('sh'	'ur'		'#'		'uh/r')
		(''		'ur'		'#'		'y/uh/r')
		(''		'ur'		''		'er')
		(''		'u'		'^'		'ah')
		(''		'u'		'^^'		'ah')
		(''		'uy'	''		'ay')
		(' g'	'u'		'#'		'')
		('g'		'u'		'%'		'')
		('g'		'u'		'#'		'w')
		('#n'	'u'		''		'y/uw')
		('t'		'u'		''		'uw')
		('s'		'u'		''		'uw')
		('r'		'u'		''		'uw')
		('d'		'u'		''		'uw')
		('l'		'u'		''		'uw')
		('z'		'u'		''		'uw')
		('n'		'u'		''		'uw')
		('j'		'u'		''		'uw')
		('th'	'u'		''		'uw')
		('ch'	'u'		''		'uw')
		('sh'	'u'		''		'uw')
		(''		'u'		''		'y/uw')
	) collect: [ :each | self fromArray: each]