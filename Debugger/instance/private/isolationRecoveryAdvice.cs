isolationRecoveryAdvice
	"Return a notifier message string to be presented in case of recovery from recursive error by revoking the changes in an isolation layer.  This surely ranks as one of Squeak's longer help messages."

	^ 'Warning! You have encountered a recursive eror situation.

Don''t panic, but do read the following advice.  If you were just fooling around, the simplest thing to do is to quit and NOT save, and restart Squeak.  If you care about recovery, then read on...

In the process of diagnosing one error, further errors occurred, making it impossible to give you a debugger to work with.  Squeak has jumped to an outer project where many of the objects and code changes that might have caused this problem are not involved in normal operation.  If you are looking at this window, chances are that this first level of recovery was successful.  If there are changes you care a lot about, try to save them now.  Then, hopefully, from the state in this debugger, you can determine what the problem was and fix it.  Do not save this image until you are confident of its recovery.

You are no longer in the world that is damaged.  The two most likely causes of recursive errors are malformed objects (for instance a corrupt value encountered in any display of the desktop) and recurring code errors (such as a change that causes errors in any attempt to display the desktop).

In the case of malformed objects, you can attempt to repair them by altering various bindings in the corrupted environment.  Open this debugger and examine the state of the objects closest to the error.

In the case of code errors, note that you are no longer in a world where the erroneous code is in effect.  The only simple option available is for you to browse to the changeSet for the project in distress, and remove one or more of the changes (later it will be possible to edit the code remotely from here).

If you feel you have repaired the problem, then you may proceed from this debugger.  This will put you back in the project that failed with the changes that failed for another try.  Note that the debugger from which you are proceeding is the second one that occurred;  you will likely find the first one waiting for you when you reenter the failed project!  Also note that if your error occurred while displaying a morph, it may now be flagged as undisplayable (red with yellow cross);  if so, use the morph debug menu to choose ''start drawing again''.

If you have not repaired the problem, you should close this debugger and delete the failed project after retrieving whatever may be of value in it.

Good luck.

	- The Squeak Fairy Godmother

PS:  If you feel you need the help of a quantum mechanic, do NOT close this window.  Instead, the best thing to do (after saving anything that seems safe to save) would be to use the ''save as...'' command in the world menu, and give it a new image name, such as OOPS.  There is a good chance that someone who knows their way around Squeak can help you out.
'