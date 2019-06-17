using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NoMoreTombs
{
    public class TombGlobalProjectile : GlobalProjectile
    {
		public override void SetDefaults(Projectile projectile)
		{
			if (!NoMoreTombs.Config.NoTombstones)
			{
				return;
			}

			switch (projectile.type)
			{
				case ProjectileID.Tombstone:
				case ProjectileID.GraveMarker:
				case ProjectileID.CrossGraveMarker:
				case ProjectileID.Headstone:
				case ProjectileID.Gravestone:
				case ProjectileID.Obelisk:
				case ProjectileID.RichGravestone1:
				case ProjectileID.RichGravestone2:
				case ProjectileID.RichGravestone3:
				case ProjectileID.RichGravestone4:
				case ProjectileID.RichGravestone5:
					projectile.active = false;
					return;
			}
		}
    }
}
