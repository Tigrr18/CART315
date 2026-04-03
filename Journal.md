# Journal
[Week 1](#week-1:-make-a-thing) | [Week 2](#week-2:-catchamall) | [Week 3](#week-3-prototyping-mechanics) | [Week 4](#week-4) | [Week 5](#week-5-returning-to-an-old-game-jam-to-review--fix-animations) | [Extra Entry](#extra-entry-hollow-knight-original) | [Week 6](#week-6-prototyping-the-final-game) | [Week 7](#week-7-furthering-prototypes-testing-methods-and-starting-to-build-assets) | [Week 8](#week-8-more-sketching-and-texturing) | [Week 9](#week-9) | [Week 10](#week-10-unity-unity-unity-did-i-mention-unity)
## Week 1: Make a Thing

I originally thought about making a small game on bitsby or another platform that I knew nothing about. After exploring my options, I felt a bit limited, and preferred acquiring new knowledge in a platform I was already fairly comfortable in rather than learning a new platform that I would probably never use again. 

I initially wanted to make a simple thing which would be an image of the front of a refrigerator with a button and a slot with a glass, and pressing on the button would add water to the glass, until it was full and the glass would be replaced by an empty one. I didn’t have the time to make the assets for it, and I wanted to challenge myself a bit more, so I decided to make a “DDR” type rhythm game. 

Because I clearly did not have time to implement different music and time the different notes to it, I decided to simply randomize the spawning of the different arrows. I originally was going to use 4 different prefabs, one for each arrow, but after talking about it with my friend, I realized that using a single prefab but using arrays of parameters to spawn them (position, rotation, color) would be much more efficient. This allowed me to have very minimal scripting for the spawning method, simply creating a randomizer that would decide which column the arrow would spawn in, and then simply transfer the information according to the chosen column when spawning the arrow. The timing between spawns is also chosen at random within an array of possible times, and I am using an IEnumerator function called with the StartCoroutine() function. 

One thing I had some issues with is scoring the points, as the scoring is determined in the individual script contained within the arrows when they are spawned, and this value needs to be sent to the main script for addition. I ended up using static methods and a “lazy” Singleton instance (not really a full singleton, I did cut some corners) of the main script. This way, the scoring method was available for use by any method, therefore the arrow methods were able to call it to add to the score. 

 After figuring out the timing, I had some troupe with the positioning of the arrows, but ended up defining a “perfect” position for each arrow, and making the “perfect” and “good” range be a set interval from these values. A “perfect” hit will give 5 points, a “good” hit will give 2 points, and a “miss” will give -5 points. The miss only works if the arrow goes past the limit detection. The range of detection is fairly big. 

To increase replayability and allow for better players to feel like they can get to a challenge, the speed of the descent and the spawn speed of the arrows increase at every 50 arrows spawned. 

In terms of end of the game, the game ends after 10 failed notes. At the 10th failed notes, existing notes will despawn and the game will no longer spawn arrows. When resetting the game, it respawns all parameters and allows for arrows to be spawned again. 

[Back to Top](#journal)

## Week 2: CatchAMall

Due to the similarity to my previous project, I attempted to continue the CatchAMall exercise without looking at the project available on the Github. 

I started off with implementing the spawning mechanic, which I was fairly familiar with having done the DDR-like rhythm game last week. I was originally going to use four individul prefabs, but ended up using a single one, and switching the sprite whenever the GameObject was created. I started off doing the fall logic by doing a simple transform, as I had done in my previous game, but shifted towards using the built in gravity with RigidBody 2D. There was some issues with that that I couldn't get to fixing in time. 

The scoring mechanics are simple. First, the game fails if an object goes past a specific point. The GAME OVER screen unfortunately had a bug, and I couldnt not make it appear properly when the game ends. I had an issue with the collider. The objects were coliding to each other and using physics, but its as if the collider wasn't registering the basket when attempting to score.

Overall, I think this week's work allowed me to refamiliarize myself with areas of Unity I haven't touched in a while, such as the colliders and RigidBody. I look forward to learning more about them and using them more confidently and efficiently

[Back to Top](#journal)

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

[Back to Top](#journal)

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

This time, instead of using a separate script to handle the platforms, I will directly use the PlayerController script to check if the character is currently moving up or down, and make the target platforms disabled when the character is actively going up, and reactivate when the character is falling back down. Tho achieve this, I did the following:

```C#
void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("JumpThroughPlatform")) {
            if(CurrentlyFalling){
                this.GetComponent<CapsuleCollider>().enabled = false;
            } else{
                this.GetComponent<CapsuleCollider>().enabled = true;
            }
        }
    }
```

To make sure I would know when the character is going up or down, I created a method that reads the Y position of the character at intervals of 0.1 second to check if it was going up or down. The "Wait()" method referenced within the code is simply an IEnumerator that waits the time given in its parameters (here 0.1 second).

```C#
    bool CurrentlyFalling(){
        int posY1, posY2, posYdifference;

        posY1 = transform.position.y;
        Wait(0.1f);
        posY2 = transform.position.y;

        if (posY1>=posY2){
            return true;
        }
        return false;
    }
```

[Back to Top](#journal)

## Week 5: Returning to an old game jam to review & fix animations

This week, I chose to focus on reworking animations I had trouble with in my first game jam. At the time, it was my first time trying to instance objects and play animations on instanciated objects, so I had absolutely no idea how to do it. The game itself was a clicker, and I wanted to make a small bubble appear and pop whenever a click was registered. We had also originally created different variations of our assets, which would change/appear as the player would buy upgrades and multipliers in the shop. I think it would also be interesting to have some random bubbles popping over the cauldron in a passive manner. 

I think this element of animation is super important for juiciness and interaction with the game, especially in the case of a clicker game. Clicker games are barely even considered a game by some, due to its extreme simplification and limited interactivity. Adding a simple animation will allow to capture the attention and focus of the player even more, keeping them engaged in the gameplay loop. Having visual stimuli that signify that an action has been complete help the player feel more engaged with the game, and this feeling of engagement is the main thing we are looking for in a clicker game. What the game will lack in mechanics needs to be replaced by game feel and aesthetics, which is exactly what I am looking to do.

The idea of the indicator being bubbles popping not only fit the theme of the game, but also brings another layer of satisfaction. Bubbles and popping bubbles are usually reminiscent of a feeling of nostalgia, of the days we would blow soap bubbles in the park, running after them to pop them. How many times have you seen children giggle and run after bubbles to pop them? Even our pets will run after them for the satisfaction of that pop. Playing with this idea helps layer the feelings and sensations I am trying to reach as a game designer to engage the player. 

<img src="https://github.com/Tigrr18/CART315/blob/main/Images%20%26%20Videos/cauldron-02.png" width="250"> ![Bubble pop animation, sprite by Arielle Wong, animation by Alexandre Godfroy](https://github.com/Tigrr18/CART315/blob/main/Images%20%26%20Videos/bubblePop.gif)

*Image of the cauldron (main asset) and bubble animation mentionned above. The illustrations are both made in illustrator by Arielle Wong, and the bubble animation was made in after effects by Alexandre Godfroy (me).*

Although some assets were already completed, the animations themselves were slightly polished to flow a bit better. 

My gameplan as to create a script which I would attach to a prefab object that would first place the bubble at the cursor, then play the animation, and have the instance destroy itself. This would allow for the bubbles to properly be generated, play the animation, then avoid overcrowding by destroying itself. Using an instance also allows me to easily switch out the sprite/animation with another, if I want to implement the different bubble versions that we had originally made. 

### BubbleManager.cs - The Code on the Instantiated Objects

```C#
using UnityEngine;
using System.Collections;

public class BubbleManager : MonoBehaviour
{
    private Animator animator; //for anim
    private SpriteRenderer spriteRenderer; //for visibility
    private float speed = 0.2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Anim()
    {
        // Enable animation
        animator.enabled = true;

        // Play animation
        animator.Play(animationName, 0, 0f);

        //Delay to make sure the animation has time to finish playing
        Delay(1f);

        //Destroys the object when the animation is done to ensure there is no overcrowding
        Destroy(gameObject);
    }

    void Update()
    {
        //make the bubble float slightly as it is popping
        transform.position += new Vector3(0, speed * Time.deltaTime, 0);
    }
    
    
    IEnumerator Delay (float delay)
    {
        yield return new WaitForSeconds(delay);
    }
}
```

This code allows me to, when instantiating the object, enable the animation, play it, and wait a delay to make sure the animation is complete. It then destroys the object to ensure that invisible objects don't keep crowding the viewport and allows for better performances. The Update() function also adds a slight upwards translation of the bubble, making it slightly more "juicy" and more satisfying. 

### BubblePop() - The Creation of the Objects

```C#
void BubblePop() {
    //Get position of the mouse
    float mouseX = Input.GetAxis("Mouse X");
    float mouseY = Input.GetAxis("Mouse Y");

    //Create bubble at the position of the mouse
    GameObject bubble = Instantiate(bubblePopPrefab, new Vector3(mouseX, mouseY, 0), Quaternion.identity);

    bubble.GetComponent<BubbleManager>().Anim();
}
```

This code allows me to first get the x and y position of the mouse, and create a vector at which the bubble object will be created. After being created, the GameObject calls the method shown [here](#bubblemanagercs---the-code-on-the-instantiated-objects). 


Find a game that you know well or are intrigued by. What decisions have the designers made that cause the game to be interesting? Where have they failed? Think mechanically rather than thematically. What ideas/methods/techniques do you think you could borrow for future projects?

[Back to Top](#journal)

## Extra entry: Hollow Knight (original)

Hollow Knight is a platformer game released February 24, 2017. This game is widely known in the online community, joining game categories such as Metroidvania, platformer, and souls-like.

The cutesy aesthetics of little bugs might be what first makes you start playing the game, but what determines player retention of this game is definitely its difficuly. Although some might be intimidated and abandon the game, most will continue playing the game *because* of its difficulty. The game is paced so that you have time to fully master a skill before getting a new one. As your skills improve, you discover new skills, such as different dashes, wall jumping, double jumping, ect. The slow release of the mechanics throughout gameplay allow for the player to take the time to fully engage with each mechanic, and makes sure that they aren't too overwhelmed by the game. 

Another interesting aspect in the "release" of mechanics throughout the game is that through the path that is intended, they challenge your skill gradually. For example, in the tutorial area, there is platforms you must jump on to reach the continuation of the game. The platforms are placed so that each platform requires a slightly different jump to reach it. This creates a feelign of near-miss, which usually pushes the player to engage with the game even more. Since you do not lose much progress from missing (at least in the beginning of the game). As the game progresses, you lose more and more from each mistake, but knowing you were able to get there in the first place makes you believe that whatever that thing is, it is achieveable. 

To go back to the jumping mechanic, it is pretty particular in this game, as there is an aspect of control with the time your spacbar is pressed. Basically, the longer you press your spacebar, the higher (and further) you will jump. Of course, there is a limit to that height and distance, but most games that use this type of mechanic don't penalise you for always using the jump at max force. In the tutorial area that i mentionned before, it is interesting, because the first few jumps encourage you to jump full force, but the last jump necessitates control, as full force will make you miss the platform entirely. Watching my mom try this area was very interesting, as she lacks most basic knowledge of games as she doesn't play them very often. The near misses encouraged her to try again and again, and although it took her some time to realize how to experiment with the mechanic, the game forced her to experiment with it to move on and progress with the game.

Another really intersting mechanic that I have noticed with my personal gameplay of the game is the lack of health bar for bosses. Couppled with different boss phases, this encourages the player to always push further and forward. Every time you go a little further, you feel this sense of pride for beating this or that, just to be filled with dread of realizing that the boss indeed has another phase after the one you've been trying to beat for the past two hours. This really shows how sometimes, indicators aren't benificial to gameplay. Did I wish there was an indicator on phases or boss health to know how much I had left? Definitely, and multiple times. But I am certain that if they had been there, I definitely wouldn't have spent hours and hours on end on this same boss, losing the concept of time and being fulled by passion and rage. 

[Back to Top](#journal)

## Week 6: Prototyping the final game

*Note: While sending in week 7, I have noticed that the images of week 6 unlinked due to using the discord links for the images. I will be cleaning up ad replacing the pictures during the week.*

### Brainstorming

Nadia and I already had the idea to make a game together for this project. Our main ideas were the following:
- How might we simulate the stress of student life
- How might we make people realize the difficulties of going through life with disabilities
- How to make an interesting rage game with new mechanics
- How to repair the inner child through fantasy and nostalgic character design and narratives
- How might we define the impacts of the "home" or frequently visited places
- How might we manage executive dysfunction about everyday mundane tasks
- How might we build a community through music and concerts

During the part of the workshops where we were being teamed up with other people and "shoving" words together, Nat and I came up with the combination of "omnipresence of pain" and "blinded". This lead us to conceptualizing a game in which the main character is our definition of "normal": No physical or mental disabilites, living a life with no aids, in a world where disabilities is the norm. The idea behind this concept is to uncover the blindness of "normal" people to the omnipresence of pain within a disabled person's life. 

### The Idea

As mentionned above, the main concept is to shift the perspective of the player, and using their normalcy against them. Within this game, we want to avoid getting specific in the portrayal of disabilities, as getting specific becomes touchy and will require a great amount of reasearch, which isn't quite realistic within the scope and timeline that we currently have for the game. 

My personal connection to this topic comes from living with CKD (Chronic Kidney Disease) since birth, having a huge impact on my diet and the different medication I can or cannot take for mundane things (ex: I cannot take Advil). Adding to that is my diabetes, which was diagnosed in my teen years, in high-school. Navigating these new issues and problems within those critical years was definitely an experience, one that is extremely hard to understand without having lived through it. I also am diagnosed with ADHD, which has made monitoring my diabetes and general health issues much harder. 

One thing we really want to convey with this game is the fact that living with the disability isn't just about the aspects to change in your personal life. That is indeed a big part of it, but what can really weight on you is the accessibility to spaces, the misunderstandings or stereotypes of your condition(s), the judgment or pity of others, and so much more. We really want to put emphasis on how phrases like "Oh this must be so hard" or "you're so perseverant, I could never" don't show the empathy people seem to think they do. These phrases are a reminder that if we do not live with those things, we simply don't live. We aren't resilient by choice, we are out of necessity, and being looked at differently simply for existing, and for doing things necessary for this existance can really take a toll on someone. 

### Ideation

Nat, Nadia and I first started working on figuring out the aesthetic and feel for the game. We did so by making a [Pinterest page](https://pin.it/4Qoxg9Ad0). Notable images from our search are the following:

![image1](https://github.com/Tigrr18/CART315/blob/main/Images%20%26%20Videos/inspo1.png)
![image2](https://github.com/Tigrr18/CART315/blob/main/Images%20%26%20Videos/inspo2.png)
![image3](https://github.com/Tigrr18/CART315/blob/main/Images%20%26%20Videos/inspo3.png)
![image4](https://github.com/Tigrr18/CART315/blob/main/Images%20%26%20Videos/inspo4.png)
![image5](https://github.com/Tigrr18/CART315/blob/main/Images%20%26%20Videos/inspo5.png)
![image6](https://github.com/Tigrr18/CART315/blob/main/Images%20%26%20Videos/inspo6.png)
![image7](https://github.com/Tigrr18/CART315/blob/main/Images%20%26%20Videos/inspo7.png)

We also had a few games that we were looking at for aestethics, such as disco elysium (shown in the pictures above) and others on the way they are approaching the matter. We also looked at some literature that approaches the matter of disabilities and shift and ability. This can all be found in the following [link](https://www.figma.com/board/96Agl9qXBoQBG0pwlN38Df/CART315-look-feel-prototype?node-id=0-1&t=ECnj30gX93joJRnS-1).

After playing around with some images, we ended up [finalizing a palette and art style](https://www.figma.com/design/DMQOIKNevP8mTI4C5B4ILA/CART315-Look-feel-prototype-moodboard?node-id=1-2&t=T8cN86bUOYaafqvr-1), by choosing a few colors of our favorite images and unifying the color scheme between them. 

Final color palette (top part):
![color palette](https://github.com/Tigrr18/CART315/blob/main/Images%20%26%20Videos/palette.png)

To test this palette out, I made a sketch with this color palette, which ended up looking like this:

![look/feel sketch with final color palette](https://github.com/Tigrr18/CART315/blob/main/Images%20%26%20Videos/look-feel%20prototype.png)

### Division of task

This week, the division of tasks looked like this: 

- Literature Review (Nadia)
- Short story and fiction research (Nadia)
- Game inspiration Research (Nadia & Nat)
- Pinterest moodboarding (All, Mostly Nat)
- Palette extraction (Alex)
- Sketching (Alex)
- Figma prototyping (ALL)

### For Next Week

- More sketching & finalizing the look
- Testing the texturing in blender
- Testing the way the colors will look lit in the engine
- Start fleshing out the "story" of the game and understanding the mechanics behind it a bit more

[Back to Top](#journal)

## Week 7: Furthering prototypes, testing methods and starting to build assets

During the two weeks between classes, we had the time to do quite a few things. We started off by meeting on sunday and brainstorming the game further. We discussed different mechanics, as well as the different spaces the game would take place in. We made a list of assets & narrative elements needed within the different spaces. 
![photo of the brainstorm on the TAG whiteboard](https://github.com/Tigrr18/CART315/blob/main/Images%20%26%20Videos/brainstorm.jpg)

The game would consist of a few different "rooms", including an outside area that would connect all of them together. There would be a cafe, an office, a clinic, and a bedroom to represent different areas of the life of the main character. The idea is to make the character feel more and more distress, and end the game in a "familiar" environment which is their room. We were also thinking of making the palette grow colder as the game progresses, to finish with a warm toned room at the end.

My main task after that was to further test the art direction of the game. I ended up doing some texturing tests on both Blender and Procreate (using the 3D function), drawing some 2D assets, as well as designing some NPC characters.

### 3D texturing tests

#### Blender
For the texture test in Blender, it took quite a while to figure out how to get the texture paint function working. I first tried to completely delete all textures from an existing textured asset I had previously made for a game jam. This asset, a book, was textured using some proceduraly generated materials. Removing the textures did not work and would simply make the texture purple. I ended up creating a copy of the file and simply painting over the existing paint effects, and removing some of the paint nodes such as the roughness, as a leather texture on a painterly object didnt feel quite right. I didn't really like the effect that it gave me, as the "paintbrush" was impossible to change, and had very little size/opacity feedback, even when using a graphic tablet with pen pressure. Despite that, it proved to me that hand texturing our assets would definitely be interesting for the project, as it really gives an interesting aspect to it

#### Procreate
For the texture tests in Procreate, I attempted to texture the bench that Nadia made this week. There was quite a few issues with the texturing, due to the way the model was exported. Essentially, the problem was that all the elements shared the same texture png, which already is a huge issue, but also the elements that repeat themselves were not alligned, which meant the textuing was all over the place. Nadia did reexport the model, but I unfortunately didn't get the time to retry it. 
![bench image](https://github.com/Tigrr18/CART315/blob/main/Images%20%26%20Videos/Bench%20messed%20up.png)

### 2D Sketches
I did a few texture/lighting sketches to have an idea how the characters would look and be rendered. The bald head was simply to test out the color palette and style, and the old woman was to see how it would look with one of my sketches. 
![old lady](https://github.com/Tigrr18/CART315/blob/main/Images%20%26%20Videos/old%20lady%20painting.png)

I also made sketches for four different NPCs that would be present in the game. All of these sketches are front facing, and were made with a mirror tool to make the process faster. This will also make the 3D modelling process much easier, and we will add imperfections and asymetry after sculpting, and we will also add some in the texturing process. So far, I also made a side profile for one of the characters, to make sure we have everything we need for the basic scuplting. I will make some outfit/body sketches after all side profiles for the four NPCs are completed. 
![NPC sketches](https://github.com/Tigrr18/CART315/blob/main/Images%20%26%20Videos/NPC%20sketches.png)
![side profile sketch](https://github.com/Tigrr18/CART315/blob/main/Images%20%26%20Videos/side%20profile.png)

I also fixed up/modified the leaf drawing for the tree that Nadia made. 
![leaf drawing](https://github.com/Tigrr18/CART315/blob/main/Images%20%26%20Videos/Leaf.png)

### Division of task

This week, the division of tasks looked like this: 

- Narrative script & Dialogue (Nat)
- Character personality sketches (Nat - In progress)
- NPC Drafting (Alex)
- Texturing (Alex)
- Sculpting & Hard modeling (Nadia)
- Unity level design (Nadia - In progress)
- Mechanics finalization (All)
- Gameplay finalisation (All)

### For Next Week

- Finish side profile of characters
- Test out texturing in Procreate with new, better unwrapped objects
- design NPC bodies
- help out on 3D modelling as needed

[Back to Top](#journal)

## Week 8: More sketching and texturing

This week, I was limited in how much I could do, due to having a lot of deadlines on different projects, as well as developping carpel tunnel on my left hand, limitting my capacity to type, draw and generally use my computer. 

### Sketching

I sketched the side profile of the old lady NPC that will be in the game. I had some trouble drawing her because I had never drawn a character with wrinkles before, so it was a challenge to do that. I needed to look up and study a lot of references to understand hwo the skin would stretch over the face, and how it would fall and drape over itself. I also struggled to determine how much I wanted to push the wrinkles, becasue I did not want the character to feel like a cartoon, but I still want the character's appearance to give a glimpse into their life, into their personality, so I do want to exaggerate some traits to put emphasis on certain aspects of the character. 

The proportions of this charater were also hard to achieve, because I tried to make the skull look a little too small for its shape, which was very hard to translate into the side profile. I really wanted to give a little old wrinkled grandma vibe, but also didn't want to dive into stereotypes. 

### 3D texturing

First, I tried texturing the model of the bench again, with a newly exported file, which had modified UV mappings. This didn't quite work out, as the materials had not been properly reassigned. Some of the planks had the same materials as some  of the rivets, which would make some UV mappings overlap, leading to some very weird effects 

![bench messup 2]()

After trying to make it work, I decided to try my hand at a different asset, and ended up texturing the tree asset that Nadia had made last week, which is a singular object, which removes the issues of objects overlapping on the UV mapping. 

[Back to Top](#journal)

## Week 9

This week, I started setting up the unity. I had never done 3D in unity before, so i was using the following tutorials: [First tutorial](https://www.youtube.com/watch?v=41MD0s9FiXI), [Second tutorial](https://www.youtube.com/watch?v=vBWcb_0HF1c).

I ended up using the second tutorial, as that is what Nadia had first started to do, and the way the controls were assigned seemed more flexible. I didn't go very far because of a lack of time this week, with a lot of projects due at the same time as well as a lot of built up anxiety and exhaustion. The part that I did complete from that tutorial was the input system with the Input Action Editor, where I set up the Movement, Rotation, Jumping and Sprint. 

![Input Action Editor](Images & Videos/InputActionEditor_Screenshot.png)

To be honest, I struggled with a lot of procrastination this week, as well as having to deal with a few personal issues. 

For next week, I plan to work some more on the player movement, as well as set up the decor.

[Back to Top](#journal)

## Week 10: Unity, Unity, Unity... Did I Mention Unity?

As mentionned in last week's journal, the goal for this week was to setup the Unity once and for all. Unfortunately, due to end of session, I could not get to worrking on this before the Thursday (today). I finished going through [the tutorial mentionned last week](https://www.youtube.com/watch?v=vBWcb_0HF1c), which ended up giving me a nice player movement & camera handling. 

### Issues and Hardships

Going through the tutorial was fairly straightforward and easy, especially with my different extentions on VS Code, which had some impressive level of autocomplete. Combined with my fast typing speed, I was almost able to go at the speed of the tutorial itself, without pausing (or well, let's be honest, it took me twice the length of the tutorial, but that's still better than having to rewind 3 times every step). 

The first issue I ran into was that the tutorial was using headers for the different parameters of the scripts to keep it clean within the Unity, but somehow I was getting some issues with them. I originally thought I was missing a semicolon at the end, but no, it didn't need any. I looked into the specific error, and it said that the header couldn't be found. After some internet digging, I figured I hadn't imported the right namespaces/libraries or something, but headers are part of "using UnityEngine", which was indeed at the top of my code. But what could be the issue?

```C#
    [header("Input Action Assets")]
    [SerializeField] private InputActionAsset playerControls;

    [header("Action Map Name Reference")]
    [SerializeField] private string actionMapName = "Player";

    [header("Action Name References")]
    [SerializeField] private string movement = "Movement";
    [SerializeField] private string jump = "Jumping";
    [SerializeField] private string sprint = "Sprint";
    [SerializeField] private string rotation = "Rotation";
```

...Oh. The header was missing a capitalization, it's spelled "Header", not "header". It was one of those mistakes that makes you feel totally dumb. Oh well!


Then, after some code cleanup, there was another error that popped up when trying to run the game:

```
NullReferenceException: Object reference not set to an instance of an object
PlayerInputHandler.SubscribeActionValuesToInputEvents () (at Assets/Scripts/PlayerInputHandler.cs:48)
PlayerInputHandler.Awake () (at Assets/Scripts/PlayerInputHandler.cs:40)
```

The corresponding method is the following, with the specific line being the commented line:

```C#
    private void SubscribeActionValuesToInputEvents()
    {
        movementAction.performed += inputInfo => MovementInput = inputInfo.ReadValue<Vector2>();
        movementAction.canceled += inputInfo => MovementInput = Vector2.zero;

        //jumpAction.performed += inputInfo => JumpTriggered = true;
        jumpAction.canceled += inputInfo => JumpTriggered = false;

        sprintAction.performed += inputInfo => SprintTriggered = true;
        sprintAction.canceled += inputInfo => SprintTriggered = false;

        rotationAction.performed += inputInfo => RotationInput = inputInfo.ReadValue<Vector2>();
        rotationAction.canceled += inputInfo => RotationInput = Vector2.zero;
    }
```

I tried going back up to the Action Name References, since I may had named the variables improperly:
```C#
    [Header("Action Name References")]
    [SerializeField] private string movement = "Movement";
    [SerializeField] private string jump = "Jumping"; //used to be "Jump", which didn't correspond in the Input Action Editor
    [SerializeField] private string sprint = "Sprint";
    [SerializeField] private string rotation = "Rotation";
```

The names in the file are as follow:
![Input Action Editor](Images & Videos/InputActionEditor_Screenshot.png)

I also checked if I had properly attached the Input Action Component to the script, and I did, everything seemed linked properly, so I am still unsure of what I can do to fix this bug. I will probably try to fix it durring the next week.

For next week, I want to focus on fixing the player movement, and making the UI, as well as making sure that the environment is properly blocked. 

[Back to Top](#journal)


