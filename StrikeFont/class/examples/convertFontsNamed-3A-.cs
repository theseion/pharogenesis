convertFontsNamed: familyName 
	" StrikeFont convertFontsNamed: 'NewYork' "
	"This utility is for use after you have used BitFont to produce data files 
	for the fonts you wish to use.  It will read the BitFont files and then 
	write them out in strike2 (*.sf2) format which is much more compact,
	and which can be read in again very quickly."
	"For this utility to work as is, the BitFont data files must be named
	'familyNN.BF', and must reside in the same directory as the image."
	| f |
	(FileDirectory default fileNamesMatching: familyName , '*.BF') do: 
		[ :fname | 
		Transcript
			cr;
			show: fname.
		f := StrikeFont new readFromBitFont: fname.
		f writeAsStrike2named: f name , '.sf2' ]