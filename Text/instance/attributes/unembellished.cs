unembellished 
	"Return true if the only emphases are font-1 and bold"
	| font1 bold |
	font1 _ TextFontChange font1.
	bold _ TextEmphasis bold.
	Preferences ignoreStyleIfOnlyBold ifFalse:
		["Ignore font1 only or font1-bold followed by font1-plain"
		^ (runs values = (Array with: (Array with: font1)))
		or: [runs values = (Array with: (Array with: font1 with: bold)
 								with: (Array with: font1))]].

	"If preference is set, then ignore any combo of font1 and bold"
	runs withStartStopAndValueDo:
		[:start :stop :emphArray |
		emphArray do:
			[:emph | (font1 = emph or: [bold = emph]) ifFalse: [^ false]]].
	^ true