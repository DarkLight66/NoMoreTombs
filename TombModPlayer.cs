using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace NoMoreTombs
{
    public class TombModPlayer : ModPlayer
    {
        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
			if (NoMoreTombs.Config.NoDeathMessage && player.whoAmI == Main.myPlayer)
			{
				damageSource = PlayerDeathReason.LegacyEmpty();
			}
            return true;
        }
    }
}
