rotateX:  angle
	| camera matrix |
	camera := scene defaultCamera.
	matrix := B3DMatrix4x4
		rotatedBy: angle
		around: ((camera position - camera target) cross: camera up)
		centeredAt: camera target.
	camera position: (matrix localPointToGlobal: camera position).
	camera up: (matrix localPointToGlobal: camera up).
	self updateHeadlight.
	self changed.