loginAs: userName
	"Assuming that we have a valid user url; read its contents and see if the user is really there."
	| actualName userList |
	eToyAuthentificationServer ifNil:[
		self proceedWithLogin.
		^true].
	userList _ eToyAuthentificationServer eToyUserList.
	userList ifNil:[
		self inform:
'Sorry, I cannot find the user list.
(this may be due to a network problem)
Please hit Cancel if you wish to use Squeak.'.
		^false].
	"case insensitive search"
	actualName  _ userList detect:[:any| any sameAs: userName] ifNone:[nil].
	actualName isNil ifTrue:[
		self inform: 'Unknown user: ',userName.
		^false].
	Utilities authorName: actualName.
	eToyAuthentificationServer eToyUserName: actualName.
	self proceedWithLogin.
	^true