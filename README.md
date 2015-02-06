# BoardBots

This is a coding game in which players code up tic-tac-toe bots in .Net and have them play each other in a tournament.

To see it in action, call RunGame.bat.

To have a real game:

1. Create one or more bots which implement the BoardBots.Shared.IPlayer interface, compiled to a single dll.  There are base solutions in the 'Base Bot Solutions' folder to make this quicker.  
2. Put the bots in a folder with BoardBots.exe and BoardBots.Shared.dll (or just in the GameFiles folder, which already has these files).  You need at least two bots.  There are a few test bots around the place if you need a second one, for example BoardBots.RandomPlayer.dll in the GameFiles folder.  
3. Run BoardBots.exe  

When playing a tournament, some stuff will happen:

## The bots will load

    Players Loaded

    BoardBots.RandomPlayer.RandomPlayer  
    GrantsAwesomeFSharpBot.GrantsAwesomeFSharpBot

    [Press any key to continue..]

## The tournament will run

    Tournament in Progress

    Game 1: RandomPlayer vs GrantsAwesomeFSharpBot.. GrantsAwesomeFSharpBot Wins!  
    Game 2: GrantsAwesomeFSharpBot vs RandomPlayer.. GrantsAwesomeFSharpBot Wins!  

    [Press any key to see the results..]

## The results will be shown

    Tournament Results

    +-------------------------------------------------------------------+  
    ¦ Player                     ¦ Played ¦ Won ¦ Drawn ¦ Lost ¦ Points ¦  
    +----------------------------+--------+-----+-------+------+--------¦  
    ¦ GrantsAwesomeFSharpBot     ¦      2 ¦   2 ¦     0 ¦    0 ¦      4 ¦  
    +----------------------------+--------+-----+-------+------+--------¦  
    ¦ RandomPlayer               ¦      2 ¦   0 ¦     0 ¦    2 ¦      2 ¦  
    +-------------------------------------------------------------------+  

    [Press any key to continue..]
