englishARules
	^ #((''		'a'		' '		'ax')
		(' '		'are'	' '		'aa/r')
		(' '		'ar'		'o'		'ax/r')
		(''		'ar'		'#'		'eh/r')
		('^'		'as'		'#'		'ey/s')
		(''		'a'		'wa'	'ax')
		(''		'aw'	''		'ao')
		(' :'		'any'	''		'eh/n/iy')
		(''		'a'		'^+#'	'ey')
		('#:'	'ally'	''		'ax/l/iy')
		(' '		'al'		'#'		'ax/l')
		(''		'again'	''		'ax/g/eh/n')
		('#:'	'ag'		'e'		'ih/jh')
		(''		'a'		'^+:#'	'ae')
		(' :'		'a'		'^+ '	'ey')
		(' '		'arr'	''		'ax/r')
		(''		'arr'	''		'ae/r')
		(' :'		'ar'		' '		'aa/r')
		(''		'ar'		' '		'er')
		(''		'ar'		''		'aa/r')
		(''		'air'	''		'eh/r')
		(''		'ai'		''		'ey')
		(''		'ay'		''		'ey')
		(''		'au'		''		'ao')
		('#:'	'al'		' '		'ax/l')
		('#:'	'als'	' '		'ax/l/z')
		(''		'alk'	''		'ao/k')
		(''		'al'		'^'		'ao/l')
		(' :'		'able'	''		'ey/b/ax/l')
		(''		'able'	''		'ax/b/ax/l')
		(''		'ang'	'+'		'ey/n/jh')
		('^'		'a'		'^#'		'ey')
		(''		'a'		''		'ae')
	) collect: [ :each | self fromArray: each]