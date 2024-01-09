## Homa Test
#### One Unity UI assets optimization. (Optimize Draw Calls)
- Combined the Timer Ui Backgrounds into a single sprite, 
decreasing overdraw. This won't decrease draw calls count, since 
previously the three Backgrounds would be batched into a single draw call.
<br/><br/>
This new combined sprite is also power of 2, this way Unity can compress it
further (previous background texture was not power of two).


- Added a canvas to the Timer Ui. Since the timer is a part of the Ui 
that changes frequently, adding a canvas to it makes sure Unity does not 
have to rebuild the whole Ui every time the timer changes.