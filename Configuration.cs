using System.ComponentModel;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader.Config;

namespace NoMoreTombs
{
	public class Configuration : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ServerSide;

		public static Configuration Instance;

		[DefaultValue(true)]
		[Label("Disable Tombstones")]
		[Tooltip("Prevents tombstones from being spawned when you die\nDefaults to true")]
        public bool NoTombstones;

		[DefaultValue(false)]
		[Label("Disable Death Message")]
		[Tooltip("Prevents death messages from being shown when you die\nDefaults to false")]
		public bool NoDeathMessage;

		[DefaultValue(false)]
		[Label("Enable Tombs From Town NPC Deaths")]
		[Tooltip("Allows town NPCs to drop tombstones when they die\nDefaults to false")]
		public bool TownNPCTombs;

		private bool OldNoTombstones;
		private bool OldNoDeathMessage;
		private bool OldTownNPCTombs;

		public override void OnLoaded()
		{
			OldNoTombstones = NoTombstones;
			OldNoDeathMessage = NoDeathMessage;
			OldTownNPCTombs = TownNPCTombs;
		}

		public override void OnChanged()
		{
			if (OldNoTombstones != NoTombstones)
			{
				mod.Logger.Info((NoTombstones ? "Disabled" : "Enabled") + " Tombstones");
			}
			if (OldNoDeathMessage != NoDeathMessage)
			{
				mod.Logger.Info((NoDeathMessage ? "Disabled" : "Enabled") + " Death Messages");
			}
			if (OldTownNPCTombs != TownNPCTombs)
			{
				mod.Logger.Info((TownNPCTombs ? "Enabled" : "Disabled") + " Town NPC Tombs");
			}
		}
		
		public override bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref string message)
		{
			if (!IsPlayerLocalServerOwner(whoAmI))
			{
				message = "Sorry, config settings can only be changed by the server owner.";
				return false;
			}
			return true;
		}

		public static bool IsPlayerLocalServerOwner(int whoAmI)
		{
			if (Main.netMode == NetmodeID.MultiplayerClient)
			{
				return Netplay.Connection.Socket.GetRemoteAddress().IsLocalHost();
			}

			for (int i = 0; i < Main.maxPlayers; i++)
			{
				RemoteClient client = Netplay.Clients[i];
				if (client.State == 10 && i == whoAmI && client.Socket.GetRemoteAddress().IsLocalHost())
				{
					return true;
				}
			}
			return false;
		}
	}
}
