using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;

namespace NoMoreTombs
{
	public static class TombDetours
	{
		public static void ApplyDetours()
		{
			On.Terraria.Player.DropTombstone += Player_DropTombstone;
			IL.Terraria.Player.KillMe += Player_KillMe;
			IL.Terraria.NPC.checkDead += NPC_checkDead;
		}

		private static void Player_DropTombstone(On.Terraria.Player.orig_DropTombstone orig, Player self, int coinsOwned, NetworkText deathText, int hitDirection)
		{
			if (!Configuration.Instance.NoTombstones)
			{
				orig(self, coinsOwned, deathText, hitDirection);
			}
		}

		private static void Player_KillMe(ILContext il)
		{
			var c = new ILCursor(il);
			var label = c.DefineLabel();

			if (!c.TryGotoNext(
				i => i.MatchCall(typeof(ChatHelper), nameof(ChatHelper.BroadcastChatMessage)),
				i => i.MatchBr(out label)))
				return;

			if (!c.TryGotoPrev(MoveType.After, i => i.MatchStloc(5)))
				return;

			c.EmitDelegate<Func<bool>>(NoDeathMessage);

			c.Emit(OpCodes.Brtrue, label);
		}

		private static bool NoDeathMessage() => Configuration.Instance.NoDeathMessage;

		private static void NPC_checkDead(ILContext il)
		{
			var c = new ILCursor(il);

			if (!c.TryGotoNext(MoveType.Before,
				i => i.MatchLdloc(3),
				i => i.MatchBrfalse(out _),
				i => i.MatchLdarg(0),
				i => i.MatchLdloc(6)))
				return;

			c.MoveAfterLabels();

			c.Emit(OpCodes.Ldarg_0);

			c.Index++;

			c.EmitDelegate<Func<NPC, bool, bool>>(TownNPCTombs);
		}

		private static bool TownNPCTombs(NPC npc, bool canDrop)
		{
			if (npc.type == NPCID.Angler || npc.type == NPCID.Princess || NPCID.Sets.IsTownPet[npc.type])
				return false;

			return canDrop ? !Configuration.Instance.NoTownNPCTombs : Configuration.Instance.TownNPCTombs;
		}
	}
}