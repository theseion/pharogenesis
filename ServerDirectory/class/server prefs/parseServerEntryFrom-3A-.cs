parseServerEntryFrom: stream
	
	| server type directory entries serverName |
	self flag: #etoyCleaningLeftToDo. 
	entries := ExternalSettings parseServerEntryArgsFrom: stream.

	serverName := entries at: 'name' ifAbsent: [^nil].
	directory := entries at: 'directory' ifAbsent: [^nil].
	type := entries at: 'type' ifAbsent: [^nil].
	type = 'file'
		ifTrue: [
			server := self determineLocalServerDirectory: directory.
			entries at: 'userListUrl' ifPresent:[:value | 
						" I do not know what is userListUrl so I just comment that for etoy removal- stephane.ducasse." 
													 "server eToyUserListUrl: value" ].
			entries at: 'baseFolderSpec' ifPresent:[:value | 
					" I do not know what is userListUrl so I just comment that for etoy removal- stephane.ducasse." 
				"server eToyBaseFolderSpec: value"].
			^ true].
	type = 'http'
		ifTrue: [server := HTTPServerDirectory new type: #ftp].
	type = 'ftp'
		ifTrue: [server := ServerDirectory new type: #ftp].

	server directory: directory.
	entries at: 'server' ifPresent: [:value | server server: value].
	entries at: 'user' ifPresent: [:value | server user: value].
	entries at: 'group' ifPresent: [:value | server groupName: value].
	entries at: 'passwdseq' ifPresent: [:value | server passwordSequence: value asNumber].
	entries at: 'url' ifPresent: [:value | server altUrl: value].
	entries at: 'loaderUrl' ifPresent: [:value | server loaderUrl: value].
	entries at: 'acceptsUploads' ifPresent: [:value | server acceptsUploads: value asLowercase = 'true'].
	entries at: 'userListUrl' ifPresent:[:value | server eToyUserListUrl: value].
	entries at: 'encodingName' ifPresent:[:value | server encodingName: value].
	ServerDirectory addServer: server named: serverName.
