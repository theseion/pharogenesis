transformPrimitivePosition: pVertex by: matrix
	"Transform the normal of the given primitive vertex"
	| x y z rx ry rz rw |
	self var: #pVertex declareC:'float *pVertex'.
	self var: #matrix declareC:'float *matrix'.
	self var: #x declareC:'double x'.
	self var: #y declareC:'double y'.
	self var: #z declareC:'double z'.
	self var: #rx declareC:'double rx'.
	self var: #ry declareC:'double ry'.
	self var: #rz declareC:'double rz'.
	self var: #rw declareC:'double rw'.

	x _ pVertex at: PrimVtxPositionX.
	y _ pVertex at: PrimVtxPositionY.
	z _ pVertex at: PrimVtxPositionZ.

	rx _ (x * (matrix at: 0)) + (y * (matrix at: 1)) + (z * (matrix at: 2)) + (matrix at: 3).
	ry _ (x * (matrix at: 4)) + (y * (matrix at: 5)) + (z * (matrix at: 6)) + (matrix at: 7).
	rz _ (x * (matrix at: 8)) + (y * (matrix at: 9)) + (z * (matrix at: 10)) + (matrix at: 11).
	rw _ (x * (matrix at: 12)) + (y * (matrix at: 13)) + (z * (matrix at: 14)) + (matrix at: 15).

	rw = 1.0 ifTrue:[
		pVertex at: PrimVtxPositionX put: (self cCoerce: rx to: 'float').
		pVertex at: PrimVtxPositionY put: (self cCoerce: ry to:'float').
		pVertex at: PrimVtxPositionZ put: (self cCoerce: rz to: 'float').
	] ifFalse:[
		rw = 0.0 
			ifTrue:[rw _ 0.0]
			ifFalse:[rw _ 1.0 / rw].
		pVertex at: PrimVtxPositionX put: (self cCoerce: rx*rw to:'float').
		pVertex at: PrimVtxPositionY put: (self cCoerce: ry*rw to:'float').
		pVertex at: PrimVtxPositionZ put: (self cCoerce: rz*rw to: 'float').
	].
