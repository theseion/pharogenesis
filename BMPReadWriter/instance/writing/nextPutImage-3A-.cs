nextPutImage: aForm
	| bhSize rowBytes rgb data colorValues depth image ppw scanLineLen |
	depth := aForm depth.
	[#(1 4 8 32) includes: depth] whileFalse:[depth := depth + 1 asLargerPowerOfTwo].
	image := aForm asFormOfDepth: depth.
	image unhibernate.
	bhSize _ 14.  "# bytes in file header"
	biSize _ 40.  "info header size in bytes"
	biWidth := image width.
	biHeight := image height.
	biClrUsed _ depth = 32 ifTrue: [0] ifFalse:[1 << depth].  "No. color table entries"
	bfOffBits _ biSize + bhSize + (4*biClrUsed).
	rowBytes _ ((depth min: 24) * biWidth + 31 // 32) * 4.
	biSizeImage _ biHeight * rowBytes.

	"Write the file header"
	stream position: 0.
	stream nextLittleEndianNumber: 2 put: 19778.  "bfType = BM"
	stream nextLittleEndianNumber: 4 put: bfOffBits + biSizeImage.  "Entire file size in bytes"
	stream nextLittleEndianNumber: 4 put: 0.  "bfReserved"
	stream nextLittleEndianNumber: 4 put: bfOffBits.  "Offset of bitmap data from start of hdr (and file)"

	"Write the bitmap info header"
	stream position: bhSize.
	stream nextLittleEndianNumber: 4 put: biSize.  "info header size in bytes"
	stream nextLittleEndianNumber: 4 put: image width.  "biWidth"
	stream nextLittleEndianNumber: 4 put: image height.  "biHeight"
	stream nextLittleEndianNumber: 2 put: 1.  "biPlanes"
	stream nextLittleEndianNumber: 2 put: (depth min: 24).  "biBitCount"
	stream nextLittleEndianNumber: 4 put: 0.  "biCompression"
	stream nextLittleEndianNumber: 4 put: biSizeImage.  "size of image section in bytes"
	stream nextLittleEndianNumber: 4 put: 2800.  "biXPelsPerMeter"
	stream nextLittleEndianNumber: 4 put: 2800.  "biYPelsPerMeter"
	stream nextLittleEndianNumber: 4 put: biClrUsed.
	stream nextLittleEndianNumber: 4 put: 0.  "biClrImportant"
	biClrUsed > 0 ifTrue: [
		"write color map; this works for ColorForms, too"
		colorValues _ image colormapIfNeededForDepth: 32.
		1 to: biClrUsed do: [:i |
			rgb _ colorValues at: i.
			0 to: 24 by: 8 do: [:j | stream nextPut: (rgb >> j bitAnd: 16rFF)]]].

	depth < 32 ifTrue: [
		"depth = 1, 4 or 8."
		data _ image bits asByteArray.
		ppw _ 32 // depth.
		scanLineLen _ biWidth + ppw - 1 // ppw * 4.  "# of bytes in line"
		1 to: biHeight do: [:i |
			stream next: scanLineLen putAll: data startingAt: (biHeight-i)*scanLineLen+1.
		].
	] ifFalse: [
		1 to: biHeight do:[:i |
			data _ (image copy: (0@(biHeight-i) extent: biWidth@1)) bits.
			1 to: data size do: [:j | stream nextLittleEndianNumber: 3 put: (data at: j)].
			1 to: (data size*3)+3//4*4-(data size*3) do: [:j | stream nextPut: 0 "pad to 32-bits"]
		].
	].
	stream position = (bfOffBits + biSizeImage) ifFalse: [self error:'Write failure'].
	stream close.