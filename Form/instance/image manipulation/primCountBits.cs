primCountBits
	"Count the non-zero pixels of this form."
	depth > 8 ifTrue:
		[^(self asFormOfDepth: 8) primCountBits].
	^ (BitBlt current toForm: self)
		fillColor: (Bitmap with: 0);
		destRect: (0@0 extent: width@height);
		combinationRule: 32;
		copyBits