copy: destRectangle from: sourcePt in: sourceForm rule: rule 
	"Make up a BitBlt table and copy the bits."
	(BitBlt current toForm: self)
		copy: destRectangle
		from: sourcePt in: sourceForm
		fillColor: nil rule: rule