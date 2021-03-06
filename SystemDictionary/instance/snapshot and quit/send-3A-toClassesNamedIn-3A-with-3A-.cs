send: startUpOrShutDown toClassesNamedIn: startUpOrShutDownList with: argument
	"Send the message #startUp: or #shutDown: to each class named in the list.
	The argument indicates if the system is about to quit (for #shutDown:) or if
	the image is resuming (for #startUp:).
	If any name cannot be found, then remove it from the list."

	| removals class |
	removals := OrderedCollection new.
	startUpOrShutDownList do:
		[:name |
		class := self at: name ifAbsent: [nil].
		class == nil
			ifTrue: [removals add: name]
			ifFalse: [class isInMemory ifTrue:
						[[class perform: startUpOrShutDown with: argument]
								on: Exception
								do: [:ex | SmalltalkImage current hasDisplay
										ifTrue: [ex pass]
										ifFalse: [Smalltalk
												at: #Console
												ifPresent: [:console | console printNl: ex description].
											Smalltalk
												logError: ex printString
												inContext: thisContext
												to: 'PharoDebug.log']]]]].

	"Remove any obsolete entries, but after the iteration"
	startUpOrShutDownList removeAll: removals