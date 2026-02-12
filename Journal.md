# Journal

## Week 1: Make a Thing

I originally thought about making a small game on bitsby or another platform that I knew nothing about. After exploring my options, I felt a bit limited, and preferred acquiring new knowledge in a platform I was already fairly comfortable in rather than learning a new platform that I would probably never use again. 

I initially wanted to make a simple thing which would be an image of the front of a refrigerator with a button and a slot with a glass, and pressing on the button would add water to the glass, until it was full and the glass would be replaced by an empty one. I didn’t have the time to make the assets for it, and I wanted to challenge myself a bit more, so I decided to make a “DDR” type rhythm game. 

Because I clearly did not have time to implement different music and time the different notes to it, I decided to simply randomize the spawning of the different arrows. I originally was going to use 4 different prefabs, one for each arrow, but after talking about it with my friend, I realized that using a single prefab but using arrays of parameters to spawn them (position, rotation, color) would be much more efficient. This allowed me to have very minimal scripting for the spawning method, simply creating a randomizer that would decide which column the arrow would spawn in, and then simply transfer the information according to the chosen column when spawning the arrow. The timing between spawns is also chosen at random within an array of possible times, and I am using an IEnumerator function called with the StartCoroutine() function. 

One thing I had some issues with is scoring the points, as the scoring is determined in the individual script contained within the arrows when they are spawned, and this value needs to be sent to the main script for addition. I ended up using static methods and a “lazy” Singleton instance (not really a full singleton, I did cut some corners) of the main script. This way, the scoring method was available for use by any method, therefore the arrow methods were able to call it to add to the score. 

 After figuring out the timing, I had some troupe with the positioning of the arrows, but ended up defining a “perfect” position for each arrow, and making the “perfect” and “good” range be a set interval from these values. A “perfect” hit will give 5 points, a “good” hit will give 2 points, and a “miss” will give -5 points. The miss only works if the arrow goes past the limit detection. The range of detection is fairly big. 

To increase replayability and allow for better players to feel like they can get to a challenge, the speed of the descent and the spawn speed of the arrows increase at every 50 arrows spawned. 

In terms of end of the game, the game ends after 10 failed notes. At the 10th failed notes, existing notes will despawn and the game will no longer spawn arrows. When resetting the game, it respawns all parameters and allows for arrows to be spawned again. 

## Week 2: CatchAMall

Due to the similarity to my previous project, I attempted to continue the CatchAMall exercise without looking at the project available on the Github. 

I started off with implementing the spawning mechanic, which I was fairly familiar with having done the DDR-like rhythm game last week. I was originally going to use four individul prefabs, but ended up using a single one, and switching the sprite whenever the GameObject was created. I started off doing the fall logic by doing a simple transform, as I had done in my previous game, but shifted towards using the built in gravity with RigidBody 2D. There was some issues with that that I couldn't get to fixing in time. 

The scoring mechanics are simple. First, the game fails if an object goes past a specific point. The GAME OVER screen unfortunately had a bug, and I couldnt not make it appear properly when the game ends. I had an issue with the collider. The objects were coliding to each other and using physics, but its as if the collider wasn't registering the basket when attempting to score.

Overall, I think this week's work allowed me to refamiliarize myself with areas of Unity I haven't touched in a while, such as the colliders and RigidBody. I look forward to learning more about them and using them more confidently and efficiently

## Week 3: Prototyping mechanics

First of all, I wanted to try to play around with the "Pawng" game that we had studied in class. I had a few ideas on how the mechanics could be changed and played with in an interesting way:
- single player pong (has a button to switch sides on the paddle)
- gravity single player (paddle controlled by mouse mouvement, the idea is to be like when we would rebound a ping pong ball on a paddle for as long as possible without making the ball fall)
- coop pong: you gain points from exchanges together, but speed changes at random
- player vs AI pong

Considering the time I had available to work on the project, I decided to try my hand at making a Player VS AI version of the game. 

First off, I used a tag to differenciate the ball, to be able to fetch the position of the ball. My "AI" was going to be mainly based on knowing **where** the ball is, and **when** it would start knowing it.

### Attempt 1:

My first attempt used different boolean flags, switching between going towards or away from the exact position of the ball. I eventually also tried leaving it completely immobile, just to have a bit more randomization.

To make sure the paddle wouldn't constantly be flickering between the options, I tried to use timers to flip between modes, with a Random.Range(). I originally tried implementing this through a counter.

```C#
if (randomTimer <= 0) {
            randomTimer = Random.Range(100,200);
            isOpposite = !isOpposite;
        } else {
            randomTimer--;
            if (isOpposite){
                MovePaddleOpposite();
            } else {
                MovePaddle();
            }
        }
}
```

### Attempt 2:

After speaking to a classmate and sharing our prototypes, I got the feedback that instead of flipping between modes, changing the target position would be much simpler. I tried a few things, but ended up landing on the following: 

```C#
void RamdomizeTargetPosition() {
    ballYPos = GetBallYPosition();
    targetYPos = ballYPos + Random.Range(-1.5f, 1.5f);
    targetYPos = Mathf.Round(targetYPos * 10f) / 10f; // Round to nearest 0.1
}
```

I messed around with having a timer or not, but ended up deciding against it. I ran into multiple bugs, needing to use way too many Debug.Log() functions to figure out what the issue was.

It ended up working much better after all the debuging.

## Week 4: 

This week, I first tried to establish a UI for the breakout game that we played around with in class. I had some trouble getting the buttons and the scenes linked properly, but ended up making it work. I had some issues with properly linking the buttons, but ended up figuring it out after watching a tutorial. Switching the scenes around wasn't too difficult, it was one of the easier parts for me. I also tested out importing different fonts into Unity. Figuring out how to install the fonts properely was a bit of a challenge, but it ended up being worth it.

For fun, I went back to a game I made a couple of years ago (a platformer), attempting to fix some issues I previously had with the colliders. Originally, I tried making it possible for the player to jump through specific types of platforms (leaves and bridge) and not jump through others (floating islands). When I origially made the game, I failed to implement the jump through mechanic, as I was not using tags properly. 

My original method to detect if the platform should be activated or not was the following:
```C#
void JumpThrough()
    {
        // Check if the player is grounded and touching the platform layer
        if (playerController != null && playerController.isGrounded && IsTouchingPlatformLayer())
        {
            // Disable the player's collider temporarily
            playerController.col.enabled = false;

            // Enable the collider after a short delay (adjust the delay as needed)
            Invoke("EnablePlayerCollider", 0.5f);
        }
    }
```

The "IsTouchingPlatformLayer()" method mentionned in the if statement is the following:
```C#
bool IsTouchingPlatformLayer()
    {
        // Check if the player is touching the platform layer using Physics2D
        Collider2D collider = Physics2D.OverlapCircle(playerController.col.bounds.center, playerController.col.bounds.extents.x, platformLayer);

        return collider != null;
    }
```

This time, instead of using a separate script to handle the platforms, I will directly use the PlayerController script to check if the character is currently moving up or down, and make the target platforms disabled when the character is actively going up, and reactivate when the character is falling back down. 
