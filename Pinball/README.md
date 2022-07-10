# Pinball
This pinball project is the base system of something I am developing into a larger product. It is built on Unity.
It is a blend between a classic pinball game and an incremental game, with buying upgrades to earn exponentially more money, and playing good pinball to magnify that income.
It uses some tools and art from asset packs I own, most notably I was using this project as a way to start learning to use Feel from MoreMountains. Otherwise I have created everything running the game myself.
Pinball.apk is a built game file that can be downloaded and run.
I have included a folder of the scripts I personally wrote for this project.

### This early build features:
    - A running pinball table with Bumpers, Paddles, Pinballs, and Scoring
        - A easy way to quickly itterate new variants of all of the above
        - Ball Spawning which in build is set to five balls/types
            - It can be scaled to any number of ball types, and any number of those on the table at a time with the modification of two floats which will scale all other relevant objects.
    - A Menu, Upgrade, and Stat System for running the upgrade path of the pinballs
        - Set up for easy design of new Stat types, and Ball Variants
        - Runs of a multipurpose scaling class that is designed to make scaling of values system wide simple
