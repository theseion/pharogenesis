buttonNormalBorderStyleFor: aButton
	"Return the normal button borderStyle for the given button."

	|pc mc ic|
	pc := self buttonColorFor: aButton.
	mc := aButton isDefault
		ifTrue: [self defaultButtonBorderColor]
		ifFalse: [pc alphaMixed: 0.7 with: Color black].
	ic := aButton isDefault
		ifTrue: [self defaultButtonBorderColor alpha: 0.5]
		ifFalse: [Color white alpha: 0.3]. 
	^(CompositeBorder new width: 1)
		borders: {RoundedBorder new
					cornerRadius: 2;
					width: 1;
					baseColor: mc.
				RoundedBorder new
					cornerRadius: 1;
					width: 1;
					baseColor: ic}