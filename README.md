## Homa Test
### 1. Implement two optimizations on the project. Even though the project runs smoothly, make two classic optimizations.
#### 1.1 One Unity UI assets optimization. (Optimize Draw Calls)
- Combined the Timer Ui Backgrounds into a single sprite, 
decreasing overdraw. This won't decrease draw calls count, since 
previously the three Backgrounds would be batched into a single draw call.
This new combined sprite is also power of 2, this way Unity can compress it
further (previous background texture was not power of two).

- Added a canvas to the Timer Ui. Since the timer is a part of the Ui 
that changes frequently, adding a canvas to it makes sure Unity does not 
have to rebuild the whole Ui every time the timer changes.

#### 1.2 Add a pooling system for the barrels
- Added a simple pooling class for prefabs `PrefabPool` wrapping the Unity's
`ObjectPool`.

- Since the whole GameScene is being reloaded for each level, pooled 
instances need to live outside that scene. Using `FxPool` as reference 
I created a static class `TowerTilesPool`, and for convenience I 
used the `DontDestroyOnLoad` method to keep instances alive.

- Since there are different types of barrels, I added an enum `TowerTileType`
for being able to know which pool to use for each type.

- Also had to tweak different systems that relied on the barrels being
destroyed.

### 2. We would like to add missions to the game in order to improve the retention of our players.
- Added a button on the start menu to open the missions panel. Once a completed mission is claimed, a new one appears.
</br> </br>
![image](https://github.com/Guillemsc/Homa-Test/assets/17142208/adcb7fdd-4c36-4038-9e96-31d4b507e72a)

- Missions data is divided in 3 parts: Configuration (`IMissionConfiguration`), Logic (`IMission`), and Save data (`MissionSaveData`). Missions are configured with scriptable objects,
which ten are converted into runtime logic. Every time a change is detected on the missions, they get saved into disk.
When opening the game, the previous missions state is loaded.

- Each mission has some difficulty and some reward.

- The whole system is built so mission addition is data driven, and adding new missions, and new types of missions is easy.

- Remark:
  - The visitor pattern is used in some parts of the code. [More info](https://en.wikipedia.org/wiki/Visitor_pattern).
