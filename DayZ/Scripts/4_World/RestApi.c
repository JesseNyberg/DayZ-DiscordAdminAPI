class Command {
	string commandName;
	string argument1;
	string argument2;
	string argument3;
}

class CommandCallBack : RestCallback
{
	protected ref RestEventManager restEventManager;
	
		//------------------------------------------------------------------------------------------------
	override void OnError(int errorCode)
	{
		Print("COMMANDAPI: OnError()");
	}

	//------------------------------------------------------------------------------------------------
	override void OnTimeout()
	{
		Print("COMMANDAPI: OnTimeout()");
	}

	//------------------------------------------------------------------------------------------------
	override void OnSuccess(string data, int dataSize)
	{
		
		if (data == "{}") return;
		
		if (!data) return;

		Print("COMMANDAPI: OnSuccess() - data size = " + dataSize + " bytes");
		
		Command command = new Command();
		JsonFileLoader<Command>.JsonLoadData(data, command);
		
		bool commandFinished = false; 
		
		APICommands apiCommands = new APICommands();
		restEventManager = new RestEventManager();
		
		if (command.commandName && command.argument1) {
			Print("COMMANDAPI: " + command.commandName + command.argument1 + command.argument2);
			
			switch (command.commandName) {
				
				case "broadcast":
					commandFinished = apiCommands.BroadcastCommand(command);
					restEventManager.ExecuteReset(commandFinished);
					break;
					
				case "teleport":
					commandFinished = apiCommands.TeleportCommand(command);
					restEventManager.ExecuteReset(commandFinished);
					break;
					
				case "teleportcoordinate":
					commandFinished = apiCommands.TeleportCoordinateCommand(command);
					restEventManager.ExecuteReset(commandFinished);
					break;
				
				case "spawn":
					commandFinished = apiCommands.SpawnItemCommand(command);
					restEventManager.ExecuteReset(commandFinished);
					break;
					
				case "kickplayer":
					commandFinished = apiCommands.KickCommand(command);
					restEventManager.ExecuteReset(commandFinished);
					break;
					
				case "banplayer":
					commandFinished = apiCommands.BanCommand(command);
					restEventManager.ExecuteReset(commandFinished);
					break;
					
				case "unbanplayer":
					commandFinished = apiCommands.UnbanCommand(command);
					restEventManager.ExecuteReset(commandFinished);
					break;
					
				case "healplayer":
					commandFinished = apiCommands.HealCommand(command);
					restEventManager.ExecuteReset(commandFinished);
					break;
					
				default:
					Print("COMMANDAPI: Unknown command.");
					restEventManager.ExecuteReset(commandFinished);
					break;
			}
			
		} else if (command.commandName == "playerlist") {
			array<Man> playerList = apiCommands.PlayerListCommand(command);
			Print(playerList[0]);
			restEventManager.InformPlayerList(playerList);
			
		} else {
			Print("COMMANDAPI: The JSON data is incorrect.");
			restEventManager.ExecuteReset(commandFinished);
		}
		
	}
	
}

class RestEventManager
{
	protected ref CommandCallBack commandCallBack;
	
	void ExecuteRequest() {
		if (!GetRestApi())
			CreateRestApi();
		
		if (!commandCallBack)
			commandCallBack = new CommandCallBack();

		RestContext commandContext = GetRestApi().GetRestContext(DISCORD_API_URL);
		
		commandContext.GET(commandCallBack, "getdata");
	}
	
	void ExecuteReset(bool commandFinished) {
		if (!GetRestApi())
			CreateRestApi();
		
		RestContext commandContext = GetRestApi().GetRestContext(DISCORD_API_URL);
		commandContext.SetHeader("\r\napi_key: " + DISCORD_API_KEY + "\r\nContent-Type: application/json");
		
        string resetPayload = "{\"commandName\":\"reset\"}";
		
		commandContext.POST(NULL, "postdata", resetPayload);
		
		GetGame().GetCallQueue(CALL_CATEGORY_SYSTEM).CallLater(InformDiscord, 3000, false, commandFinished);
	}
	
	void InformDiscord(bool commandFinished) {
		if (!GetRestApi())
			CreateRestApi();
		
		RestContext commandContext = GetRestApi().GetRestContext(DISCORD_API_URL);
		commandContext.SetHeader("\r\napi_key: " + DISCORD_API_KEY + "\r\nContent-Type: text/plain");
		
		string informPayload;
		
		if (commandFinished) {
			informPayload = "OK";
		} else {
			informPayload = "FAIL";
		}
			
		commandContext.POST(NULL, "discordpostdata", informPayload);
	}
	
	void InformPlayerList(array<Man> players) {
		if (!GetRestApi())
			CreateRestApi();
		
		RestContext commandContext = GetRestApi().GetRestContext(DISCORD_API_URL);
		commandContext.SetHeader("\r\napi_key: " + DISCORD_API_KEY + "\r\nContent-Type: text/plain");
	
		string informPayload;
		
		
		if (players) {
			informPayload = "Players:\n";
			
			foreach (Man player : players) {
				informPayload += player.GetIdentity().GetName() + "\n";
			}
			commandContext.POST(NULL, "discordpostdata", informPayload);
		}
	}
	
}