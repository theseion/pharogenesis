stopAfterMSecs: mSecs
	"Terminate this sound this note after the given number of milliseconds."

	count _ (mSecs * self samplingRate) // 1000.
