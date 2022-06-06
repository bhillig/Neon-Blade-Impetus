# Game Basic Information #

## Summary ##

Our protagonist, the player, begins their journey within virtual space. 
We do not know how they got here or how they were created. 
Yet, we understand one thing, their entity is trapped in this digital landscape and they must resist this elusive environment to survive.  
This realm, while mimicking reality, is a hostile environment, surrounded by various obstacles that the player must unravel. 
It is in a sense, a digital prison from which must you escape. 
As the player, you must attempt to break out of this landscape paralleling the reality of a modern, yet futuristic city. 
As you traverse through the area, you must creatively move to get through obstacles blocking your path kill the enemies along your path to get to the end.

Overall, the idea of the game is a 3D platformer game involving many different mechanics for the player to traverse through the landscape. The objective of the game is to traverse through the level and reach the end. 

They can reach the end through various movement mechanics such as wallriding, dashing, sliding in order to avoid obstacles along the way.
They can kill enemies along their path by dashing against them in order to get through to the next stage of levels. There are 5 levels in total. Once they reach the end of level 5, they complete the game. 

## Gameplay Explanation ##

### Introduction: 
Our core gameplay mechanic is staged around movement. We intended to include various unique movements that help the traversal through the level. The core gameplay focuses on physics and the game states the trigger based upon specific movement events throughout the game. 

### Main Movement Input: 
- For our core movement, we designed it with similar inputs seen in most 3D games of moving the character, left, right, backward, and forward. 
- **WASD** controls the movement in this game. 
- By **moving the mouse**, we can interact with the camera (which looks specifically at the player). Moving the mouse left or right respectively will interact with the camera rotation around the player. 
- By moving and pressing **SHIFT**, the player can slide in this game. By sliding the player can lower their height and be under and obstacles along the path. 

### WallRunning: 
- We designed our level with the intention of wall running being our main movement for traversal. 
- By holding the forward or **W** key and having a wall next to you will cause the player to wall ride on the wall. 
- Pressing the **jump key** or **spacebar** and while wall riding will cause the player to detach from the wall and trigger a wall jump. 
- A wall jump is exactly as intended, it is a jump from a wall. 

### Dashing: 
- Dashing is a charging mechanic. It is done with two steps. 
- By **left clicking** or using the Fire1 button, the player charges his/her dash attack. Once charged, the player will give a visual and audio indicator. 
- Once the player has given the visual/audio indicator, it specifies the player has charged their dashing mechanic. 
- Once charged, the player has around 0.5 seconds to dash against the target. 
- If the player is not target locked towards an enemy, the player with dash a short distance. 
- If the player is target locked towards an enemy, the player will dash at a longer distance through the enemy itself. 

### Level Design: 
- The stage itself is linear in the design and there will be a linear progression throughout the story. The player will know where to go at different times and will progress by moving through the level itself. 

#### Platforms: 
- Mainly the platforms serve as a way for the player to stand on. They act as terrain for the player to traverse through. 
- Platforms can either be buildings, flat platforms, or ramps. 

#### Hazards: 
- Indicated by their red wall glow, hazards are screens that kill the player if they come into contact with it. It is best to avoid these. 
 
#### Walls: 
- Walls acts as terrain for the player to wall ride. These are placed throughout the map for the player to traverse through. 
- Walls can be just horizontal walls or cylinder walls. Both can wall ride. 

### Enemies (Turrets): 
- Turrets are placed throught the map. They act as enemies and will shoot projectiles towards the player. If the projectile hit the player, they will die. 
- When the player comes into range of the turret, it will start shooting at the player. 
- When the player becomes close to the turret, the turret will slow down it's fire rate to balance changes. 
- If the player dashes through the turret, the turret will be killed. 

# Main Roles #

Your goal is to relate the work of your role and sub-role in terms of the content of the course. Please look at the role sections below for specific instructions for each role.

Below is a template for you to highlight items of your work. These provide the evidence needed for your work to be evaluated. Try to have at least 4 such descriptions. They will be assessed on the quality of the underlying system and how they are linked to course content. 

*Short Description* - Long description of your work item that includes how it is relevant to topics discussed in class. [link to evidence in your repository](https://github.com/dr-jam/ECS189L/edit/project-description/ProjectDocumentTemplate.md)

Here is an example:  
*Procedural Terrain* - The background of the game consists of procedurally-generated terrain that is produced with Perlin noise. This terrain can be modified by the game at run-time via a call to its script methods. The intent is to allow the player to modify the terrain. This system is based on the component design pattern and the procedural content generation portions of the course. [The PCG terrain generation script](https://github.com/dr-jam/CameraControlExercise/blob/513b927e87fc686fe627bf7d4ff6ff841cf34e9f/Obscura/Assets/Scripts/TerrainGenerator.cs#L6).

You should replay any **bold text** with your relevant information. Liberally use the template when necessary and appropriate.

## User Interface

**Describe your user interface and how it relates to gameplay. This can be done via the template.**

## Movement/Physics

**Describe the basics of movement and physics in your game. Is it the standard physics model? What did you change or modify? Did you make your movement scripts that do not use the physics system?**

## Animation and Visuals

**List your assets including their sources and licenses.**

**Describe how your work intersects with game feel, graphic design, and world-building. Include your visual style guide if one exists.**

## Input

**Describe the default input configuration.**

**Add an entry for each platform or input style your project supports.**

## Game Logic

**Document what game states and game data you managed and what design patterns you used to complete your task.**

# Sub-Roles

## Audio

**List your assets including their sources and licenses.**

**Describe the implementation of your audio system.**

**Document the sound style.** 

## Gameplay Testing

**Add a link to the full results of your gameplay tests.**

**Summarize the key findings from your gameplay tests.**

## Narrative Design

**Document how the narrative is present in the game via assets, gameplay systems, and gameplay.** 

## Press Kit and Trailer

**Include links to your presskit materials and trailer.**

**Describe how you showcased your work. How did you choose what to show in the trailer? Why did you choose your screenshots?**



## Game Feel

**Document what you added to and how you tweaked your game to improve its game feel.**