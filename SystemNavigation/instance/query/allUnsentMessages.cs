allUnsentMessages
	"SystemNavigation new allUnSentMessages"
	"Answer the set of selectors that are implemented by some object in the  
	system but not sent by any."
	^ self allUnSentMessagesWithout: {{}. {}}