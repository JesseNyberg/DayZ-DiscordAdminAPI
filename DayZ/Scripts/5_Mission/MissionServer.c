modded class MissionServer
{
	ref RestEventManager restEventManager;
	
	ref JSONConfig jsonConfig;

    override void OnInit()
    {
        super.OnInit();
        
		jsonConfig = new JSONConfig();
		
		MakeAPIConfig();
		
		GetGame().GetCallQueue(CALL_CATEGORY_GAMEPLAY).CallLater(performGetRequest, 10000, true);
		
    }
	
	void MakeAPIConfig() {
		
		string filePath = DISCORD_API_CONFIG_FOLDER + "ApiURL.json";
		
		if (FileExist(filePath)) {
			JsonFileLoader<JSONConfig>.JsonLoadFile(filePath, jsonConfig);
			DISCORD_API_URL = jsonConfig.ApiURL;
			DISCORD_API_KEY = jsonConfig.ApiKey;
		} else {
			MakeDirectory(DISCORD_API_CONFIG_FOLDER);
			JsonFileLoader<JSONConfig>.JsonSaveFile(filePath, jsonConfig);
		}
	}
	
	void performGetRequest() {
		if (!DISCORD_API_URL) return;
		
		if (!restEventManager)
			restEventManager = new RestEventManager();
		
		restEventManager.ExecuteRequest();
	}

}

class JSONConfig {
	string ApiURL;
	string ApiKey;
}

