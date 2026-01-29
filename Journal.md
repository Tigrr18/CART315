Week 1: Make a Thing

I originally thought about making a small game on bitsby or another platform that I knew nothing about. After exploring my options, I felt a bit limited, and preferred acquiring new knowledge in a platform I was already fairly comfortable in rather than learning a new platform that I would probably never use again. 

I initially wanted to make a simple thing which would be an image of the front of a refrigerator with a button and a slot with a glass, and pressing on the button would add water to the glass, until it was full and the glass would be replaced by an empty one. I didn’t have the time to make the assets for it, and I wanted to challenge myself a bit more, so I decided to make a “DDR” type rhythm game. 

Because I clearly did not have time to implement different music and time the different notes to it, I decided to simply randomize the spawning of the different arrows. I originally was going to use 4 different prefabs, one for each arrow, but after talking about it with my friend, I realized that using a single prefab but using arrays of parameters to spawn them (position, rotation, color) would be much more efficient. This allowed me to have very minimal scripting for the spawning method, simply creating a randomizer that would decide which column the arrow would spawn in, and then simply transfer the information according to the chosen column when spawning the arrow. The timing between spawns is also chosen at random within an array of possible times, and I am using an IEnumerator function called with the StartCoroutine() function. 

One thing I had some issues with is scoring the points, as the scoring is determined in the individual script contained within the arrows when they are spawned, and this value needs to be sent to the main script for addition. I ended up using static methods and a “lazy” Singleton instance (not really a full singleton, I did cut some corners) of the main script. This way, the scoring method was available for use by any method, therefore the arrow methods were able to call it to add to the score. 

 After figuring out the timing, I had some troupe with the positioning of the arrows, but ended up defining a “perfect” position for each arrow, and making the “perfect” and “good” range be a set interval from these values. A “perfect” hit will give 5 points, a “good” hit will give 2 points, and a “miss” will give -5 points. The miss only works if the arrow goes past the limit detection. The range of detection is fairly big. 

To increase replayability and allow for better players to feel like they can get to a challenge, the speed of the descent and the spawn speed of the arrows increase at every 50 arrows spawned. 

In terms of end of the game, the game ends after 10 failed notes. At the 10th failed notes, existing notes will despawn and the game will no longer spawn arrows. When resetting the game, it respawns all parameters and allows for arrows to be spawned again. 
