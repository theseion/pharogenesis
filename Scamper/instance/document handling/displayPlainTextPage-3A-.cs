displayPlainTextPage: newSource
	"treat as plain text"
	pageSource _ newSource content.
	document _ nil.
	formattedPage _ pageSource withSqueakLineEndings.
	backgroundColor _ self defaultBackgroundColor.
	currentUrl _ newSource url.

	self status: 'sittin'.
	self changeAll: 	#(currentUrl relabel hasLint lint formattedPage formattedPage
formattedPageSelection).
	^true