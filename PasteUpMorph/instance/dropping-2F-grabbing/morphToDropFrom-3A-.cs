morphToDropFrom: aMorph
	| itsSelector aScriptor adjustment anEditor actualObject aUserScript ownersChoice aNail representee |

	owner ifNotNil:
		[(ownersChoice _ owner substituteForMorph: aMorph beingDroppedOn: self)
			ifNotNil:	[^ ownersChoice]].

	self alwaysShowThumbnail ifTrue:
		[aNail _ aMorph representativeNoTallerThan: self maxHeightToAvoidThumbnailing norWiderThan: self maximumThumbnailWidth thumbnailHeight: self heightForThumbnails.
		aNail == aMorph ifFalse:
			[aNail position: (self primaryHand position - ((self primaryHand targetOffset - self primaryHand formerPosition) * (aNail extent / aMorph extent)) rounded)].
		^ aNail].

	((aMorph isKindOf: MorphThumbnail) and: [(representee _ aMorph morphRepresented) owner == nil])
		ifTrue:
			[representee position: (self primaryHand position - ((self primaryHand targetOffset - self primaryHand formerPosition) * (representee extent / aMorph extent)) rounded).
			^ representee].

	self expandPhrasesToScripts ifFalse: [^ aMorph].

	(aMorph hasProperty: #newPermanentScript) ifTrue: [^ self emptyPermanentScriptorFrom: aMorph].

	(aMorph isKindOf: PhraseTileMorph) ifFalse: [^ aMorph].
	aMorph isCommand ifFalse: [^ aMorph].
	(actualObject _ aMorph actualObject) ifNil: [^ aMorph].
	actualObject assureUniClass.

	itsSelector _ aMorph userScriptSelector.
	aScriptor _ (itsSelector ~~ nil and: [itsSelector size > 0])
		ifTrue:
			[actualObject isFlagshipForClass
				ifFalse:
					["We can set the status for our instantiation of this script, but cannot allow script editing"
					anEditor _ actualObject scriptEvaluatorFor: itsSelector phrase: aMorph.
					adjustment _ 50 @ 40.
					anEditor]
				ifTrue:
					["old note: ambiguous case: if there's a script editor on the world, drop down a button, else drop down the script editor"
					aUserScript _ actualObject class userScriptForPlayer: actualObject selector: itsSelector.
					aUserScript isTextuallyCoded
						ifTrue: [^ self scriptorForTextualScript: itsSelector ofPlayer: actualObject].
					((anEditor _ actualObject scriptEditorFor: itsSelector) isInWorld and:
							[anEditor owner == self])
						ifFalse:
							[adjustment _ 50 @ 30.
							anEditor]
						ifTrue:
							[adjustment _ 60 @ 20.
							actualObject permanentScriptEditorFor: aMorph]]]

		ifFalse:   "It's a system-defined selector; construct an anonymous scriptor around it"
			[adjustment _ 60 @ 20.
			actualObject permanentScriptEditorFor: aMorph].

	aScriptor position: (self primaryHand position - adjustment).
	(aScriptor isMemberOf: ScriptEditorMorph) ifTrue:
		[aScriptor playerScripted expungeEmptyUnRenamedScripts].
	^ aScriptor