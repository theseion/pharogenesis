applyUpdatesFromDisk
	"Utilities applyUpdatesFromDisk"
	"compute highest update number"
	| updateDirectory updateNumbers |
	updateDirectory := self getUpdateDirectoryOrNil.
	updateDirectory
		ifNil: [^ self].
	updateNumbers := updateDirectory fileNames
				collect: [:fn | fn initialIntegerOrNil]
				thenSelect: [:fn | fn notNil].
	self
		applyUpdatesFromDiskToUpdateNumber: (updateNumbers
				inject: 0
				into: [:max :num | max max: num])
		stopIfGap: false