using Terraria.ModLoader;

namespace NoMoreTombs
{
	public class NoMoreTombs : Mod
	{
		public override void Load()
		{
			TombDetours.ApplyDetours();
		}
	}
}
