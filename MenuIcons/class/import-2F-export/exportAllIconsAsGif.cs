exportAllIconsAsGif
	"self exportAllIconsAsGif"

	| sels | 
	sels := self class selectors select: [:each |  '*Icon' match: each asString].
	sels do: [:each | self exportIcon: (MenuIcons perform: each) asGifNamed: each asString].
