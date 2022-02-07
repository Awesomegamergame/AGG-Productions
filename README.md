# AGG-Productions

Welcome to AGG Productions Launcher this launcher is a replacement for the old chaotic launcher which was old slow and just very plain. This launcher also now allows other people that are working on there own game to be able to upload it here and have some cool auto apdating and havee there game hosted for free (it supports any game made for windows Ex: Unity unreal and even native coded games, as long as the game can be started as an .exe)

# Building / Contributing

How to Add Features and to fix bugs in AGG-Productions

1. Fork the repo
2. Use Visual Studio 2019 with .netframwork 4.7.2
3. Fix the bug or add the feature
4. Test to make sure if it works correctly

Other info
- (Only read if adding another feature) Remember that this launcher must be able to add games without changing the code each time.

# Old Launcher's Location

https://chaoticdev.weebly.com/

# How To Add Your Game To AGG-Productions

1. Fork this repository.
2. Upload each version of your game to anywhere you want (github releases is recommended because i think its unlimited space there).
3. Copy the Chaotic.html update board as a template or make your own and upload it to your fork of the repo beside the rest of them.
4. Grab the raw link of the update board but replace your github username with awesomegamergame and put the link in the updates.json file.
5. For your versions of your game grab each link of the just like step 4 but create a file with "YourGameName.json" in the gameversion folder.
6. Get the link of the created json file from step 5 and add the link into the games.json file.
7. Create a image for the game button the resolution of the image must be 191x50 pixels then upload it to the images folder in the repo and put the link in the ButtonData.json with your gamename
8. Create a pull request to my repo with your changes you made in your i will review the changes to make sure they are right if they are it will be merged if its not right then ill tell you whats wrong so you can fix it.
9. When it gets merged into my repo then you should see your game in the launcher.

Other Info
- if your game gets merged into the launcher and you dont see it wait about 10 mins and if its not there still create an issue in my repo and ill help you fix it.
- if your game links change you can change the links in your repo and repull request the changed links into my repo.
- if the game that your are adding is a real game that you arent making or don't have permission to upload here than i will deny your pull request and it will not be added.
- also you MUST have a image for the button and a update board for your game written in html using the .html extension
