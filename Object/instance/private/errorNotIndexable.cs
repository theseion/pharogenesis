errorNotIndexable
	"Create an error notification that the receiver is not indexable."

	self error: ('Instances of {1} are not indexable' translated format: {self class name})