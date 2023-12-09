class CfgPatches
{
	class DiscordCommandAPI
	{
		units[] = {};
		weapons[] = {};
		requiredVersion = 0.1;
		requiredAddons[] = {};
	};
};

class CfgMods
{
	class DiscordCommandAPI
	{
		type = "mod";
		dependencies[] = {"Game","World","Mission"};
		class defs
		{
			class gameScriptModule
			 {
				 files[] = {"Discord_CommandAPI/Scripts/3_Game"};
			 };
			class worldScriptModule
			{
				files[] = {"Discord_CommandAPI/Scripts/4_World"};
			};
			class missionScriptModule
			{
				files[] = {"Discord_CommandAPI/Scripts/5_Mission"};
			};
		};
	};
};