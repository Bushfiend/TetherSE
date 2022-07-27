using System;
using HarmonyLib;

namespace TetherSE
{
    public class Patches
    {
        public static void UseObjectPatch(float useDistance)
        {
            if(useDistance > maxUseDistance)
            {
                useDistance = maxUseDistance;
            }

            var info = AccessTools.Field("VRage.Game.MyConstants:DEFAULT_INTERACTIVE_DISTANCE");
            info.SetValue(null, useDistance);
        }



        public static float maxUseDistance = 500f;
    }
}
