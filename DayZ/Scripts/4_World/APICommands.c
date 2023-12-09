class APICommands {
	
	bool BroadcastCommand(Command command)
	{
		if (command.argument2 && command.argument3) {
			ExpansionNotification(command.argument1, command.argument3, EXPANSION_NOTIFICATION_ICON_INFO, COLOR_EXPANSION_NOTIFICATION_INFO, command.argument2.ToInt()).Create();
			return true;
		}

		return false;
	}

	bool TeleportCommand(Command command)
	{
		if (command.argument2)
		{
			array<Man> players = new array<Man>;
			GetGame().GetPlayers(players);
			PlayerBase player1;
			PlayerBase player2;

			foreach (Man player : players)
			{
				if (player.GetIdentity().GetName() == command.argument1)
					player1 = PlayerBase.Cast(player);

				if (player.GetIdentity().GetName() == command.argument2)
					player2 = PlayerBase.Cast(player);
			}

			if (player1 && player2)
			{
				player1.SetPosition(player2.GetPosition());
			} else {
				Print("COMMANDAPI: Some of the players weren't found.");
				return false;
			}
			
			return true;
		}
		
		return false;
	}
	
	bool TeleportCoordinateCommand(Command command)
	{
		if (command.argument2)
		{
			array<Man> players = new array<Man>;
			GetGame().GetPlayers(players);
			PlayerBase targetPlayer;

			foreach (Man player : players)
			{
				if (player.GetIdentity().GetName() == command.argument1)
					targetPlayer = PlayerBase.Cast(player);
			}

			if (targetPlayer)
			{
				targetPlayer.SetPosition(command.argument2.ToVector());
			} else {
				Print("COMMANDAPI: The player wasn't found.");
				return false;
			}
			
			return true;
		}
		
		return false;
	}
	
	bool KickCommand(Command command) {
		
		if (command.argument2) {
			array<Man> players = new array<Man>;
			GetGame().GetPlayers(players);
			PlayerBase targetPlayer;
			
			foreach (Man player : players)
			{
				if (player.GetIdentity().GetName() == command.argument1)
					targetPlayer = PlayerBase.Cast(player);
			}
			
			GetRPCManager().VSendRPC( "RPC_MissionGameplay", "KickClientHandle", new Param1<string>( command.argument2 ), true, targetPlayer.GetIdentity());
			
			return true;
		}
		
		return false;
	}
	
	bool BanCommand(Command command) {
		
		if (command.argument2) {
			array<Man> players = new array<Man>;
			GetGame().GetPlayers(players);
			PlayerBase targetPlayer;
			
			foreach (Man player : players)
			{
				if (player.GetIdentity().GetName() == command.argument1)
					targetPlayer = PlayerBase.Cast(player);
			}
			
			if (!targetPlayer) return false;
			
			BanDuration banDuration = GetBansManager().GetCurrentTimeStamp();
			string banAuthorDetails = string.Format("%1|%2","Discord","1337");
			banDuration.Permanent = true;
			
			string tgId = targetPlayer.GetIdentity().GetPlainId();
			
			if (GetBansManager().AddToBanList(new BannedPlayer(GetPermissionManager().GetPlayerBaseByID(tgId).VPlayerGetName(), tgId, GetPermissionManager().GetPlayerBaseByID(tgId).VPlayerGetHashedId() ,banDuration,banAuthorDetails, command.argument2)))
				GetPermissionManager().NotifyPlayer("1337","Player "+GetPermissionManager().GetPlayerBaseByID(tgId).VPlayerGetName()+ " for the reason: " + command.argument2,NotifyTypes.NOTIFY);
			else {
				GetPermissionManager().NotifyPlayer("1337","#VSTR_ERROR_BAN_PLAYER "+GetPermissionManager().GetPlayerBaseByID(tgId).VPlayerGetName(),NotifyTypes.NOTIFY);
				return false;
			}	
			
			return true;
		}
		
		return false;
	}
	
	bool UnbanCommand(Command command) {
		GetBansManager().RemoveFromBanList(command.argument1);
		return true;
	}
	
	bool HealCommand(Command command) {
		array<Man> players = new array<Man>;
		GetGame().GetPlayers(players);
		PlayerBase targetPlayer;
			
		foreach (Man player : players)
		{
			if (player.GetIdentity().GetName() == command.argument1)
				targetPlayer = PlayerBase.Cast(player);
		}
		
		if (!targetPlayer) {
			return false;
		}
		
		targetPlayer.SetHealth(targetPlayer.GetMaxHealth("", ""));
		targetPlayer.SetHealth( "","Blood", targetPlayer.GetMaxHealth("","Blood"));
		targetPlayer.SetHealth("","Shock", targetPlayer.GetMaxHealth("","Shock"));
		targetPlayer.GetStatHeatComfort().Set(targetPlayer.GetStatHeatComfort().GetMax());
		targetPlayer.GetStatTremor().Set(targetPlayer.GetStatTremor().GetMin());
		targetPlayer.GetStatWet().Set(targetPlayer.GetStatWet().GetMin());
		targetPlayer.GetStatEnergy().Set(targetPlayer.GetStatEnergy().GetMax());
		targetPlayer.GetStatWater().Set(targetPlayer.GetStatWater().GetMax());
		targetPlayer.GetStatDiet().Set(targetPlayer.GetStatDiet().GetMax());
		targetPlayer.GetStatSpecialty().Set(targetPlayer.GetStatSpecialty().GetMax());
		targetPlayer.HealBrokenLegs();
		return true;
	}
	
	array<Man> PlayerListCommand(Command command) {
		array<Man> players = new array<Man>;
		GetGame().GetPlayers(players);
		return players;
	}

	bool SpawnItemCommand(Command command)
	{
		if (command.argument2 && command.argument3)
		{
			array<Man> players = new array<Man>;
			GetGame().GetPlayers(players);
			PlayerBase targetPlayer;

			foreach (Man player : players)
			{
				if (player.GetIdentity().GetName() == command.argument1)
					targetPlayer = PlayerBase.Cast(player);
			}

			if (targetPlayer)
			{
				EntityAI inventoryCheckEntity = createEntity(command.argument2);

				if (!inventoryCheckEntity) return false;

				for (int i = 0; i < command.argument3.ToInt(); i++)
				{
					bool inventoryNotFull = targetPlayer.GetInventory().CanAddEntityToInventory(inventoryCheckEntity);

					if (inventoryNotFull)
					{
						EntityAI spawnEntity = targetPlayer.GetInventory().CreateInInventory(inventoryCheckEntity.GetType());
					}
					else
					{
						targetPlayer.SpawnEntityOnGroundPos(inventoryCheckEntity.GetType(), targetPlayer.GetPosition());
					}
				}
			} else {
				return false;
			}
			
			return true;
		}
		
		return false;
	}
	
	EntityAI createEntity(string entityName) {
		EntityAI entity = EntityAI.Cast(GetGame().CreateObject(entityName, "0 0 0", true, false, true));
		return entity;
	}
}





