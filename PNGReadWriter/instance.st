ImageReadWriter
	subclass: #PNGReadWriter
	instanceVariableNames:
		'chunk form width height depth backColor bitsPerChannel colorType interlaceMethod bitsPerPixel bytesPerScanline thisScanline prevScanline rowSize globalDataChunk unknownChunks palette transparentPixelValue filtersSeen swizzleMap cachedDecoderMap bigEndian'
	classVariableNames: 'BPP BlockHeight BlockWidth Debugging StandardColors StandardSwizzleMaps'
	poolDictionaries: ''
	category: 'Graphics-Files'