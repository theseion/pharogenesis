fromForm: aForm
	| fs |
	fs _ self form: aForm.
	fs origin: 0@0.
	fs direction: aForm width @ 0.
	fs normal: 0 @ aForm height.
	fs tileFlag: true.
	^fs