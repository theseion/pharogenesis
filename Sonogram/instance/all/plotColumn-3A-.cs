plotColumn: dataArray

	| chm1 i normVal r |
	columnForm unhibernate.
	chm1 _ columnForm height - 1.
	0 to: chm1 do:
		[:y | 
		i _ y*(dataArray size-1)//chm1 + 1.
		normVal _ ((dataArray at: i) - minVal) / (maxVal - minVal).
		normVal < 0.0 ifTrue: [normVal _ 0.0].
		normVal > 1.0 ifTrue: [normVal _ 1.0].
		columnForm bits at: chm1-y+1 put: (pixValMap at: (normVal * 255.0) truncated + 1)].
	(lastX _ lastX + 1) > (image width - 1) ifTrue:
		[self scroll].
	image copy: (r _ (lastX@0 extent: 1@image height))
			from: (32//image depth-1)@0
			in: columnForm rule: Form over.
	"self changed."
	self invalidRect: (r translateBy: self position)