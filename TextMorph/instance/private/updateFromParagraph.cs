updateFromParagraph
	"A change has taken place in my paragraph, as a result of editing and I must be updated.  If a line break causes recomposition of the current paragraph, or it the selection has entered a different paragraph, then the current editor will be release, and must be reinstalled with the resulting new paragraph, while retaining any editor state, such as selection, undo state, and current typing emphasis."
	| newStyle sel oldLast oldEditor |
	paragraph ifNil: [^ self].
	wrapFlag ifNil: [wrapFlag _ true].
	editor ifNotNil: [oldEditor _ editor.
					sel _ editor selectionInterval.
					editor storeSelectionInParagraph].
	text _ paragraph text.
	paragraph textStyle = textStyle
		ifTrue: [self fit]
		ifFalse: ["Broadcast style changes to all morphs"
				newStyle _ paragraph textStyle.
				(self firstInChain text: text textStyle: newStyle) recomposeChain.
				editor ifNotNil: [self installEditorToReplace: editor]].
	super layoutChanged.
	sel ifNil: [^ self].

	"If selection is in top line, then recompose predecessor for possible ripple-back"
	predecessor ifNotNil:
		[sel first <= (self paragraph lines first last+1) ifTrue:
			[oldLast _ predecessor lastCharacterIndex.
			predecessor paragraph recomposeFrom: oldLast to: text size delta: 0.
			oldLast = predecessor lastCharacterIndex
				ifFalse: [predecessor changed. "really only last line"
						self predecessorChanged]]].

	((predecessor~~nil and: [sel first <= self paragraph firstCharacterIndex])
		or: [successor~~nil and: [sel first > (self paragraph lastCharacterIndex+1)]])
		ifTrue:
		["The selection is no longer inside this paragraph.
		Pass focus to the paragraph that should be in control."
		self firstInChain withSuccessorsDo:
			[:m |  (sel first between: m firstCharacterIndex
								and: m lastCharacterIndex+1)
					ifTrue: [m installEditorToReplace: editor.
							^ self passKeyboardFocusTo: m]].
		self error: 'Inconsistency in text editor' "Must be somewhere in the successor chain"].
	editor ifNil:
		["Reinstate selection after, eg, style change"
		self installEditorToReplace: oldEditor]
