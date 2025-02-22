﻿using RimWorld;
using UnityEngine;
using Verse;

namespace AdeptusMechanicus
{
    // Token: 0x02000002 RID: 2
    public class TrailThrower
    {
        public static void ThrowSprayTrail(Vector3 loc, Map map, Vector3 origin, Vector3 destination, string defname = null, float size = 1.5f, int rotationRate = 240, float projectieSpeed = 0, Color? color = null)
        {
            if (map == null)
            {
                return;
            }
            if (!loc.InBounds(map))
            {
                return;
            }
            if (!GenView.ShouldSpawnMotesAt(loc, map))
            {
                return;
            }
            float forward = 0f;
            if ((destination - origin).MagnitudeHorizontalSquared() > 0.001f)
            {
                forward = (destination - origin).AngleFlat();
            }
            float backward = 0f;
            if ((origin - destination).MagnitudeHorizontalSquared() > 0.001f)
            {
                backward = (origin - destination).AngleFlat();
            }
            TrailThrower.ThrowSprayTrail(loc, map, forward, backward, defname, size, rotationRate, projectieSpeed, color);
        }

        public static void ThrowSprayTrail(Vector3 loc, Map map, float angle, float angleR, string defname = null, float size = 1.5f, int rotationRate = 240, float projectieSpeed = 0, Color? color = null)
        {
            FleckDef def = defname.NullOrEmpty() ? null : DefDatabase<FleckDef>.GetNamedSilentFail(defname);
            if (def != null)
            {
                AdeptusFleckMaker.Thrown(loc + Quaternion.AngleAxis(Rand.Range(-10, 10) + angle, Vector3.up) * Vector3.back * Rand.Range(0, projectieSpeed), map, def, size, color, Rand.Range(0, 360), rotationRate, 0f, Rand.Range(0, projectieSpeed), (float)Rand.Range(-15, 15) + angle);
            }
        }

    }
}
