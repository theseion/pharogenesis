testWordArrayWithRefStream
	array := WordArray new: 10.
	1 to: 10 do: [ :i | array at: i put: self randomWord ].
	self validateRefStream
	