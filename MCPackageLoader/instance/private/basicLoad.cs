basicLoad
	errorDefinitions := OrderedCollection new.
	[[
	
	"FIXME. Do a separate pass on loading class definitions as the very first thing.
	This is a workaround for a problem with the so-called 'atomic' loading (you wish!)
	which isn't atomic at all but mixes compilation of methods with reshapes of classes.

	Since the method is not installed until later, any class reshape in the middle *will*
	affect methods in subclasses that have been compiled before. There is probably
	a better way of dealing with this by ensuring that the sort order of the definition lists
	superclass definitions before methods for subclasses but I need this NOW, and adding
	an extra pass ensures that methods are compiled against their new class definitions."

	additions do: [:ea | self loadClassDefinition: ea] displayingProgress: 'Loading classes...'.
	
	additions do: [:ea | self tryToLoad: ea] displayingProgress: 'Compiling methods...'.
	removals do: [:ea | ea unload] displayingProgress: 'Cleaning up...'.
	self shouldWarnAboutErrors ifTrue: [self warnAboutErrors].
	errorDefinitions do: [:ea | ea addMethodAdditionTo: methodAdditions] displayingProgress: 'Reloading...'.
	methodAdditions do: [:each | each installMethod].
	methodAdditions do: [:each | each notifyObservers].
	additions do: [:ea | ea postloadOver: (self obsoletionFor: ea)] displayingProgress: 'Initializing...']
		on: InMidstOfFileinNotification 
		do: [:n | n resume: true]]
			ensure: [self flushChangesFile]