itemsForFileEnding: suffix
	| labels lines selectors |
	labels _ OrderedCollection new.
	lines _ OrderedCollection new.
	selectors _ OrderedCollection new.
	(suffix = 'bmp') | (suffix = 'gif') | (suffix = 'jpg') | (suffix =
'form') | (suffix = '*') | (suffix = 'png') ifTrue:
		[labels addAll: #('open image in a window' 'read image into ImageImports' 'open image as background').
		selectors addAll: #(openImageInWindow importImage openAsBackground)].
	(suffix = 'morph') | (suffix = 'morphs') | (suffix = 'sp') |
(suffix = '*') ifTrue:
		[labels add: 'load as morph'.
		selectors add: #openMorphFromFile.
		labels add: 'load as project'.
		selectors add: #openProjectFromFile].
	(suffix = 'extseg') | (suffix = 'project') | (suffix = 'pr') ifTrue:
		[labels add: 'load as project'.
		selectors add: #openProjectFromFile].
	(suffix = 'bo') | (suffix = '*') ifTrue:[
		labels add: 'load as book'.
		selectors add: #openBookFromFile].
	(suffix = 'mid') | (suffix = '*') ifTrue:
		[labels add: 'play midi file'.
		selectors add: #playMidiFile].
	(suffix = 'movie') | (suffix = '*') ifTrue:
		[labels add: 'open as movie'.
		selectors add: #openAsMovie].
	(suffix = 'st') | (suffix = 'cs') | (suffix = '*') ifTrue:
		[suffix = '*' ifTrue: [lines add: labels size].
		labels addAll: #('fileIn' 'file into new change set'
'browse changes' 'browse code' 'remove line feeds' 'broadcast as update').
		lines add: labels size - 1.
		selectors addAll: #(fileInSelection fileIntoNewChangeSet
browseChanges browseFile removeLinefeeds putUpdate)].
	(suffix = 'swf') | (suffix = '*') ifTrue:[
		labels add:'open as Flash'.
		selectors add: #openAsFlash].
	(suffix = 'ttf') | (suffix = '*') ifTrue:[
		labels add: 'open true type font'.
		selectors add: #openAsTTF].
	(suffix = 'gz') | (suffix = '*') ifTrue:[
		labels addAll: #('view decompressed' 'decompress to file').
		selectors addAll: #(viewGZipContents saveGZipContents)].
	(suffix = '3ds') | (suffix = '*') ifTrue:[
		labels add: 'Open 3DS file'.
		selectors add: #open3DSFile].
	(suffix = 'tape') | (suffix = '*') ifTrue:
		[labels add: 'open for playback'.
		selectors add: #openTapeFromFile].
	(suffix = 'wrl') | (suffix = '*') ifTrue:
		[labels add: 'open in Wonderland'.
		selectors add: #openVRMLFile].
	(suffix = 'htm') | (suffix = 'html') ifTrue:
		[labels add: 'open in browser'.
		selectors add: #openInBrowser].
	(suffix = '*') ifTrue:
		[labels addAll: #('generate HTML').
		lines add: labels size - 1.
		selectors addAll: #(renderFile)].
	^ Array with: labels with: lines with: selectors