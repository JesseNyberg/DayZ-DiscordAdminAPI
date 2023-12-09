# DayZ Discord Admin API


## Description
### **DayZ Discord Admin API helps you to command your server remotely!**

- #### Many DayZ API's that offer you remote command execution cost you on a monthly basis.
- #### On the other hand the DayZ Discord Admin API is free and relies only on your own hosted server.

## Background
  - #### DayZ is notorious for its lack of API capability and remote execution capabilities. You can't connect to DayZ directly, instead you get or post request to the API to get the data every X time.
  - #### That's what this API basically does: It get requests every 10 seconds, and checks if the CommandAPI (the request handling server) has a command (JSON format) for it to run.


https://github.com/JesseNyberg/DayZ-DiscordAdminAPI/assets/67522887/3253424b-491e-4362-8d15-e186c256ec0a



- ### **Numerous admin commands**: 
  - #### List of admin commands
    - #####  /broadcast (to make a notification for all players) (needs DayZ Expansion Core)
    - #####  /playerlist (to print all connected players)
    - #####  /spawn (to spawn an item to the selected player)
    - #####  /teleport (to teleport the selected player to another player)
    - #####  /teleportcoordinate (to teleport the selected player to specific coordinates)
    - #####  /healplayer (to heal the selected player)
    - #####  /kickplayer (to kick the selected player) (needs VPP Admin tools)
    - #####  /banplayer (to ban the selected player) (needs VPP Admin tools)
    - #####  /unbanplayer (to unban the selected player) (needs VPP Admin tools)
    
- ### **Completely local**: 
  - #### The API doesn't need any port forwarding!
 
- ### **Discord integration**: 
  - #### The discord handles the post requests and sends the CommandAPI the correct data according to your input. The correct data will be gathered by the DayZ Server

- ### **API Key authentication**:
  - #### While the server is local, you need an api key header to authenticate with the server.

- ### **Discord Slash Command guidelines**:
     ![image](https://github.com/JesseNyberg/DayZ-DiscordAdminAPI/assets/67522887/e3160e3d-d1ff-4ac4-8904-66070500cbba)


## Installation
#### .Net 6.0 minimum needed, also VPP Admin tools and DayZ Expansion Core, unless you plan on removing kick, ban and broadcast commands.
  - #### 1. Download the release.zip and unzip it somewhere.
  - #### 2. Add the mod to your server folder (@Discord_CommandAPI) and include it in your .bat file (-mod)
  - #### 3. Edit the appsettings.json in the CommandAPI folder and change the ApiKey.
  - #### 4. Edit the config.ini in DiscordBot folder: Get the token from https://discord.com/developers/applications (search how to do a bot if you don't have one already) and change API_KEY to what you want, other values you can leave alone, unless you know what you're doing.
  - #### 5. Run the DayZ server once and locate to DayZServer\profileFolder\DiscordAPIConfig
  - #### 6. Change the json to something like this:
  - ```
    {
    "ApiURL": "http://localhost:5000/api/",
	  "ApiKey": "fkawoik29e19sokd"
    }
    ```
  - #### 7. Restart server. Run CommandAPI and DiscordBot and try out!
  - #### 8. If you want to use this and encounter any issues, feel free to contact me.
---
