doEtoyLogin
	"Pop up the eToy login if we have a server that provides us with a known user list"

	"Find us a server who could do eToy authentification for us"
	eToyAuthentificationServer _ 
		(ServerDirectory localProjectDirectories, ServerDirectory servers values)
			detect:[:any| any hasEToyUserList]
			ifNone:[nil].
	eToyAuthentificationServer "no server provides user information"
		ifNil:[^self startUpAfterLogin].
	self prepareForLogin.
	EtoyLoginMorph 
		loginAndDo:[:userName| self loginAs: userName]
		ifCanceled:[self cancelLogin].