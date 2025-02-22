﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using Verse.AI;
using Verse.AI.Group;
using HarmonyLib;
using Verse.Sound;
using AdeptusMechanicus;
using AdeptusMechanicus.ExtensionMethods;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;

namespace AdeptusMechanicus.HarmonyInstance
{

    [HarmonyPatch(typeof(ITab_Pawn_Gear), "DrawThingRow")]
    public static class ITab_Pawn_Gear_DrawThingRow_CustomizeItemButton_Transpiler
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var instructionsList = new List<CodeInstruction>(instructions);
            MethodInfo SelPawnForGear = AccessTools.Property(typeof(ITab_Pawn_Gear), "SelPawnForGear").GetGetMethod(true);
            MethodInfo CanControlColonist = AccessTools.Property(typeof(ITab_Pawn_Gear), "CanControlColonist").GetGetMethod(true);
            for (int i = 0; i < instructionsList.Count; i++)
            {
                CodeInstruction instruction = instructionsList[i];
                if (instruction.opcode == OpCodes.Stloc_S && ((LocalBuilder)instruction.operand).LocalIndex == 11)
                {
                    yield return new CodeInstruction(opcode: OpCodes.Ldarg_3);
                    yield return new CodeInstruction(opcode: OpCodes.Ldarg_0);
                    yield return new CodeInstruction(opcode: OpCodes.Call, SelPawnForGear);
                    yield return new CodeInstruction(opcode: OpCodes.Ldarg_0);
                    yield return new CodeInstruction(opcode: OpCodes.Call, CanControlColonist);
                    yield return new CodeInstruction(opcode: OpCodes.Ldarg_1);
                    yield return new CodeInstruction(opcode: OpCodes.Call, operand: typeof(ITab_Pawn_Gear_DrawThingRow_CustomizeItemButton_Transpiler).GetMethod("CustomizeItemButton"));
                }
                yield return instruction;
            }

        }

        public static Rect CustomizeItemButton(Rect rect, Thing thing, Pawn SelPawnForGear, bool CanControlColonist, ref float y)
        {
            if (CanControlColonist)
            {
                if (thing != null)
                {
                    if (thing.def.IsApparel)
                    {
                        if (AdeptusApparelUtility.CanCustomizeApparel(thing))
                        {
                            Rect rect2 = new Rect(rect.width, y, 24f, 24f);
                            TooltipHandler.TipRegionByKey(rect2, "AdeptusMechanicus.ApparelCustomizeationOptions", thing.LabelNoCount, thing);
                            if (Widgets.ButtonImage(rect2, TexButton.CustomizeButton, true))
                            {
                                SoundDefOf.Tick_High.PlayOneShotOnCamera(null);
                                Find.WindowStack.Add(new Dialog_CustomizeApparelComposite(thing));
                            }
                        }
                    }
                    else if (thing.def.IsWeapon)
                    {

                    }
                    //    rect.width -= 24f;
                }
            }

            return rect;
        }
    }

}
