removeActionsSatisfying: aBlock

	self actionMap keys do:
		[:eachEventSelector |
			self
   				removeActionsSatisfying: aBlock
				forEvent: eachEventSelector
		]