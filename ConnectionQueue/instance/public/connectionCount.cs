connectionCount
	"Return an estimate of the number of currently queued connections. This is only an estimate since a new connection could be made, or an existing one aborted, at any moment."

	| count |
	self pruneStaleConnections.
	accessSema critical: [count := connections size].
	^ count
