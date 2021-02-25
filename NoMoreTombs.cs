using Terraria.ModLoader;
using Terraria;
using Terraria.Localization;

namespace NoMoreTombs
{
    class NoMoreTombs : Mod
    {
		internal static Configuration Config;
		public override void Unload() => Config = null;

		public override void Load()
		{
			On.Terraria.Player.DropTombstone += Player_DropTombstone;
		}

		private void Player_DropTombstone(On.Terraria.Player.orig_DropTombstone orig, Player self, int coinsOwned, NetworkText deathText, int hitDirection)
		{
			if (!Config.NoTombstones)
			{
				orig(self, coinsOwned, deathText, hitDirection);
			}
		}
	}
}
