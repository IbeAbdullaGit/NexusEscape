# Stealth
 
A co-op stealth game, where the players must communicate with eachother to help one of the escape from a Place (Place to be determined), made for GDW

Controls:
WASD to move Player 1
X to toggle split screen, arrow keys to move player 2
Click on interactable objects with mouse left click
press Z when UI elements are open, from the puzzles, to close them, or click on the interactable again

NOTE: When running the EXE, a development log console may pop up showing some errors.
This can be disregarded since the build still works despite it, so just clear this log if it shows up.

FOR GED GROUP A2:
In some parts of the video, there are some meter UI elements on the right side of the screen. This is meant to be visible only to the 2nd player of the game,
and we have a split screen toggle implemented for multiplayer testing. For the purposes of showcasing and testing specific parts, we have disabled the split
screen for now, which is why that UI is on the first players screen and is visible.

All of the managers were implemented in the way that they were to make it easy to change the answers for the puzzles on the fly, during run time. We could
easily have multiple instances of the same puzzle by having these "interfaces" that each instance has, which change the overall "answer" for the manager,
which works since there are only one UI instance for each puzzle, so there can only be one answer at a time. From example, we can go from puzzle A -> puzzle B,
during puzzle A, we access the manager and set the answer to X, and then once puzzle A is done and we activate puzzle B, we simply set the answer to Y based
on the parameters within B's interface script.
And as well, we can easily change one part of the puzzle without having to change other parts or edit the main script, due to having multiple scripts 
which interact with the main managers.

References:
Field of view: https://www.youtube.com/watch?v=j1-OyLo77ss
Patroling: https://www.youtube.com/watch?v=c8Nq19gkNfs
Library used for saving with jsons: https://github.com/nlohmann/json#examples
player controller: https://www.youtube.com/watch?v=1LtePgzeqjQ
Pushable button: https://github.com/daniel-ziorli/PhysicsButton/blob/main/PhysicsButton.cs
interaction: https://www.youtube.com/watch?v=AQc-NM2Up3M
slope movement: https://www.youtube.com/watch?v=xCxSjgYTw9c&t=246s
