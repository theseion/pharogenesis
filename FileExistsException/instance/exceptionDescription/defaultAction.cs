defaultAction
	"The default action taken if the exception is signaled."

	^ self fileClass fileExistsUserHandling: self fileName
