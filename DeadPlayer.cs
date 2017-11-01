using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace NoMoreTombs
{
    public class DeadPlayer : ModPlayer
    {
        public override bool Autoload(ref string name)
        {
            return TombsConfig.NoDeathMessage;
        }

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            damageSource = PlayerDeathReason.LegacyEmpty();
            return true;
        }
    }
}
