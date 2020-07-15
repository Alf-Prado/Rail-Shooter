# StarWolf Game
Consists in a space based game, where the player constantly moves forward while avoiding and/or shooting different objects such as inmobile mines, and space debris, but also following enemies. The movement mechanics is based on the game: Starfox.
* Single and Multiplayer mode through UNet Networking (The same level is used in both modes but object instancing number is reduced in Multiplayer for better performance).

![DemoImage2](https://github.com/Alf-Prado/Rail-Shooter-V2/blob/master/Rail%20Shooter%20V2/DemoImages/DemoImage2.png)
 
 * A Cinemachine Dolly Cart is implemented to keep a constant forward movement, limiting X and Y with the camera view
 
![DemoImage3](https://github.com/Alf-Prado/Rail-Shooter-V2/blob/master/Rail%20Shooter%20V2/DemoImages/DemoImage3.png)

- Instancing for different objects within an specified area, where every object type is assigned:
  - A random size in a range of values.
  - A different damage if the player collides.
  - Five levels of detail, reducing the polygon count for each object depending on the distance to the player.
  
![DemoImage1](https://github.com/Alf-Prado/Rail-Shooter-V2/blob/master/Rail%20Shooter%20V2/DemoImages/DemoImage1.png)
  
- The following enemy guides itself to the player with a Finite State Machine:
  - A Mesh Filter is positioned in the level, checking on each Update the player's position, assigning an approximate value to the texture, so the enemy can find the player.
  - The player's position in the mesh can be seen with a Mesh Filter texture color, but it's disabled so as not to interfere with sight.
  
![DemoImage4](https://github.com/Alf-Prado/Rail-Shooter-V2/blob/master/Rail%20Shooter%20V2/DemoImages/DemoImage4.png)

- The Mines that automatically end the game when the player collides with them are Tessellated octahedrons.
  - The octahedron is modeled inside the script with vertex, faces and connections.
  - It is tessellated 4 times, so it has the appearance of a sphere.
  - 5 spikes are randomly generated for each sphere with normalized vectors.
  
![DemoImage5](https://github.com/Alf-Prado/Rail-Shooter-V2/blob/master/Rail%20Shooter%20V2/DemoImages/DemoImage5.png)

- A rear view that shows when the player hits "3". It's a Render Texture that's positioned behind the player. When it's activated, the ship will also shoot backwards.

![DemoImage6](https://github.com/Alf-Prado/Rail-Shooter-V2/blob/master/Rail%20Shooter%20V2/DemoImages/DemoImage6.png)

### Credits:
#### Alfredo Prado Cajiga, Alonso Narro Delgado and Jorge León Salas developed the gameplay in Unity, assets and programming with the guidance of the professor, Sergio Ruiz Loza for the Modeling and Animation class at the ITESM as a final project.
