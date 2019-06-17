using Terraria.ModLoader;

namespace NoMoreTombs
{
    class NoMoreTombs : Mod
    {
		internal static Configuration Config;
		public override void Unload() => Config = null;
	}
}
