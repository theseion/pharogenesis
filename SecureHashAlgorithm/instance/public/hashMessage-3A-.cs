hashMessage: aStringOrByteArray 
	"Hash the given message using the Secure Hash Algorithm."
	^ self hashStream: aStringOrByteArray asByteArray readStream