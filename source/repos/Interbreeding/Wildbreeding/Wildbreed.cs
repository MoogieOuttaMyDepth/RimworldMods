using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using Verse.AI;

namespace RimWorld
{
	public class JobGiver_Wildbreed : ThinkNode_JobGiver
	{
		// Token: 0x06002D42 RID: 11586 RVA: 0x000FD888 File Offset: 0x000FBA88
		protected override Job TryGiveJob(Pawn pawn)
		{
			if (pawn.gender != Gender.Male || !pawn.ageTracker.CurLifeStage.reproductive)
			{
				return null;
			}
			Predicate<Thing> validator = delegate (Thing t)
			{
				Pawn pawn3 = t as Pawn;
				return !pawn3.Downed && pawn3.CanCasuallyInteractNow(false) && !pawn3.IsForbidden(pawn) && PawnUtility.FertileMateTarget(pawn, pawn3);
			};
			Pawn pawn2 = (Pawn)GenClosest.ClosestThingReachable(pawn.Position, pawn.Map, ThingRequest.ForDef(pawn.def), PathEndMode.Touch, TraverseParms.For(pawn, Danger.Deadly, TraverseMode.ByPawn, false), 30f, validator, null, 0, -1, false, RegionType.Set_Passable, false);
			if (pawn2 == null)
			{
				return null;
			}
			return JobMaker.MakeJob(JobDefOf.Mate, pawn2);
		}
	}
}