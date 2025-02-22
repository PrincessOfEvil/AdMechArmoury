﻿using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using Verse.Grammar;

namespace AdeptusMechanicus.HarmonyInstance
{
	[HarmonyPatch(typeof(IdeoFoundation_Deity), "GenerateDeities")]
	public static class IdeoFoundation_Deity_GenerateDeities_FactionSpecifc_Patch
	{
		public static bool Prefix(IdeoFoundation_Deity __instance)
		{
			if (__instance.ideo.culture.defName.StartsWith("OG_"))
			{
				DeityUtility.GenerateSpecificDeities(__instance);
				return false;
			}
			return true;
		}
	}
}
