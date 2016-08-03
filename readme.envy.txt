*** Getting started with the build environment (envy) ***
To initialize the build environment run "envy.cmd" in the root of the source tree.

To build sources run "build" command anywhere in the source tree. The build environment also automatically restores all packages from NuGet, so the 
build will succeed.

To clean the entire repository from files created while building the project run "nuke" command (be careful if you have untracked files - this command wipes out untracked files as well).

To launch the solution in Visual Studio run "sln" command.

To see all the aliases currently defined in the build environment run "aliases" command.

To deploy plugin into the destination folder within Fiddler location on the PC run "deploy" command (by default it restarts Fiddler in viewer mode with 'no-attach' mode).

To deploy plugin into the destination folder within Fiddler location on the PC and immediately start capturing request run "deploy -live" command (it restarts Fiddler in regular proxy mode).