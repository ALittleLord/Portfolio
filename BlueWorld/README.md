# BlueWorld
BlueWorld is an ocean based survival game. It is built in Unity.\
Included in this portfolio is an early test build featuring some of the mechanics I am building out.\
It is currently in a unpolished state with known bugs, and use cases that haven't been prevented.\
The Water Shader is not mine, credit for that goes to Pinwheel Studios and their Poseidon-Low Poly Water Asset. The physics based buoyancy system being used in tandem with it is mine.


### This early build features:
    - A Cleat and Rope System
        - Cleats on rafts can be selected and have ropes tied to to connect the rafts together
        - The rope will stretch up to its max length, if over max length will shrink until it is it back in range
        - The max rope length can be controlled
        - Multi Raft structures can be built by cleating multiple rafts together
    - A Crafting system
        - Crafting menu can be used to build new objects into world
        - Currently no out of editor way to properly view resources player has, player has been given basic resources to build more rafts
    - A Basic fishing system
        - Bobber casting, hook chance timing, hook context animations, fish catching
    - Basic movement system
        - Walking/Swimming
    - Very early stage of rowing/paddling

### To Play
    - Download and extract BlueWorldBuild_0.01
    - Run Blue-World.exe

### Controls
    - WASD movement
    - SPACE jump
    - SHIFT toggle movement speed
    - TAB to open crafting menu
    - CTRL to use item in hand
    - 1-9 to navigate HotBar
    - ROPING
        - Click on cleat to open context menu
        - Click grab rope to start roping from that cleat, or grab the rope attached to that cleat
        - Click on another cleat to tie down other end of rope
        - Press ESC to stop roping
            - A current bug leads to the system roping mode (see top left of screen) to often not stop when second end of rope is tied down. This causes starting another rope to not be able to be possible. Use ESC to clear roping mode if this happens.
    - PADDLING
        - Use paddle (Currently 3 on hotrbar represented by downwards arrow) to give player forwards momentum
        - This has not been disabled when player is not attached to anything
        - If the player is standing over a rigidbody, raft or boat, the player will attach to this item so the object moves with the player

