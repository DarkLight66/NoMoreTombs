using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace NoMoreTombs
{
	public class Configuration : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ClientSide;

		[DefaultValue(true)]
		[Label("Disable Tombstones")]
		[Tooltip("Prevents tombstones from being spawned when you die\nDefaults to true")]
        public bool NoTombstones;

		[DefaultValue(false)]
		[Label("Disable Death Message")]
		[Tooltip("Prevents death messages from being shown when you die\nDefaults to false")]
		public bool NoDeathMessage;

		private bool OldNoTombstones;
		private bool OldNoDeathMessage;

		public override void OnLoaded()
		{
			OldNoTombstones = NoTombstones;
			OldNoDeathMessage = NoDeathMessage;
			NoMoreTombs.Config = this;
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
		}
	}
}
