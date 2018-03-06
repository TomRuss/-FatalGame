/*IMPORTANT: You need to install Standard Assets_Characters for the demo scene to work!

Tutorial: If you are using the fps controller, you have to set up some things. First you have to make a new layer for your weapon.
Next up you need to change te first person character's camera, so it can't see the weapon layer. (Culling mask) 

After you've done that, you need to set up 2 camera's inside the first person character. Let's call the first one camera, and te second one camera02. 
On the first camera you need to set the culling mask to weapon only, and on camera02 to everything except the weapon layer. 
the first camera is depth only, min clipping planes 0.001, and depth 1. camera02 is skybox, and clipping planes and depth the same as the first camera. 
You have to apply the two scripts to the first person character. Now you have to apply the first camera to the peek script, and the second camera to the lean script.

If you want to change the controls, go to the scripts, and inside you will find: if input.getkey. There you can change the input to what you want. 

If you want to change how much the camera goes left or right, go inside the peek script and change the nextPos under input q and e. 

if you want to change how much your weapon rotates, go to the lean script inside the inspector and change the values to whatever you want.
*/
