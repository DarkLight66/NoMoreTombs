using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NoMoreTombs
{
    public class TombDestroyer : GlobalProjectile
    {
        public static readonly int[] tombProjectiles = new int[] { ProjectileID.Tombstone, ProjectileID.GraveMarker, ProjectileID.CrossGraveMarker, ProjectileID.Headstone, ProjectileID.Gravestone, ProjectileID.Obelisk, ProjectileID.RichGravestone1, ProjectileID.RichGravestone2, ProjectileID.RichGravestone3, ProjectileID.RichGravestone4, ProjectileID.RichGravestone5 };

        public override bool PreAI(Projectile projectile)
        {
            if (Array.Exists(tombProjectiles, x => x == projectile.type))
            {
                projectile.active = false;
                return false;
            }
            return base.PreAI(projectile);
        }
    }
}
