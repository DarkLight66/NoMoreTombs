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
                // Note: this empties the death message, but doesn't actually prevent a message from being sent, leading to a new empty line being added to chat.
                // Actually stopping the message from being sent may require some IL editing.
                damageSource = PlayerDeathReason.LegacyEmpty();
			}
            return true;
        }
    }
}
