using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace NoMoreTombs
{
	public class TombGlobalNPC : GlobalNPC
	{
		public override bool PreNPCLoot(NPC npc)
		{
			// Gravestone spawns for NPCs are done here since the code runs after the place it would run in 1.4
			// Doing this in NPCLoot would prevent the tombs from being spawned if PreNPCLoot returned false.
			DropTombstoneTownNPC(npc);

			return base.PreNPCLoot(npc);
		}

		public void DropTombstoneTownNPC(NPC npc)
		{
			if (Main.netMode == NetmodeID.MultiplayerClient || !Configuration.Instance.TownNPCTombs || !npc.townNPC || npc.type == NPCID.SkeletonMerchant || npc.type == NPCID.OldMan || npc.type == NPCID.Angler)
			{
				return;
			}

			NetworkText deathText = NetworkText.FromKey(Language.GetText("LegacyMisc.19").Key, new object[] { npc.GetFullNetName() });
			float randomXVel = (Main.rand.NextBool() ? 0.1f : -0.1f) * Main.rand.Next(10, 30);
			float randomXVel2 = Main.rand.Next(-35, 36) * 0.1f;
			while (randomXVel2 < 2f && randomXVel2 > -2f)
			{
				randomXVel2 += Main.rand.Next(-30, 31) * 0.1f;
			}
			int tombProjID = Main.rand.Next(6);
			if (npc.type == NPCID.Merchant || npc.type == NPCID.TaxCollector)
			{
				tombProjID = Main.rand.Next(5);
				tombProjID += ProjectileID.RichGravestone1;
			}
			else if (tombProjID == 5)
			{
				tombProjID = ProjectileID.Tombstone;
			}
			else
			{
				tombProjID += ProjectileID.GraveMarker;
			}
			int projIndex = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, randomXVel + randomXVel2, Main.rand.Next(-40, -20) * 0.1f, tombProjID, 0, 0f, Main.myPlayer);
			Main.projectile[projIndex].miscText = deathText.ToString();
		}
	}
}
