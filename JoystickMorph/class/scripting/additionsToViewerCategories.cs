additionsToViewerCategories
	"Answer a list of (<categoryName> <list of category specs>) pairs that characterize the phrases this kind of morph wishes to add to various Viewer categories."

	^ #((joystick (
(slot amount 'The amount of displacement' Number readOnly Player getAmount unused unused)
(slot angle 'The angular displacement' Number readOnly Player getAngle  unused  unused)
(slot leftRight  'The horizontal displacement' Number  readOnly Player getLeftRight  unused  unused)
(slot upDown 'The vertical displacement' Number  readOnly Player getUpDown unused unused))))


