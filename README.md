## Homa Test
#### One Unity UI assets optimization. (Optimize Draw Calls)
- Combined the Timer Ui Backgrounds into a single sprite, 
decreasing overdraw. This won't decrease draw calls count, since 
previously the three Backgrounds would be batched into a single draw call.
<br/>
This new combined sprite is also power of 2, this way Unity can compress it
further (previous background texture was not power of two).


- Added a canvas to the Timer Ui. Since the timer is a part of the Ui 
that changes frequently, adding a canvas to it makes sure Unity does not 
have to rebuild the whole Ui every time the timer changes.

<br/>

#### Add a pooling system for the barrels
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