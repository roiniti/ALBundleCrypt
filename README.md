# ALBundeCrypt

Code from shipgirl game for decoding/decrypting asset bundles, adapted to be more readable for humans.
Also it can re encode/encrypt the bundle
//NOTE: this only encodes the bundle not the wrapper, the game needs the wrapper in order to work, actually the game crashes with the bundle encrypted with this tool
# Usage 
To use ALBundeCrypt, enter the following command in your terminal:

`ALBundeCrypt -d|-e "input" ["output"]`

-d decodes the input
-e encodes the input
Where "input" is the path to the input asset bundle file, and "output" is the path to the output file where the decoded asset bundle will be saved.
