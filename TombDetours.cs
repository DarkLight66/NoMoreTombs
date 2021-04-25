using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using Terraria;
using Terraria.Localization;

namespace NoMoreTombs
{
	public static class TombDetours
	{
		public static void ApplyDetours()
		{
			On.Terraria.Player.DropTombstone += Player_DropTombstone;
			IL.Terraria.Player.KillMe += Player_KillMe;
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
				i => i.MatchCall<NetMessage>(nameof(NetMessage.BroadcastChatMessage)),
				i => i.MatchBr(out label)))
				return;

			if (!c.TryGotoPrev(MoveType.After, i => i.MatchStloc(5)))
				return;

			c.EmitDelegate<Func<bool>>(NoDeathMessage);

			c.Emit(OpCodes.Brtrue, label);
		}

		private static bool NoDeathMessage() => Configuration.Instance.NoDeathMessage;
	}
}
