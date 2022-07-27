using System;
using HarmonyLib;
using VRage.Plugins;
using Sandbox.Game.World;
using System.Collections.Generic;
using System.Linq;
using Sandbox.Game.Entities;
using Sandbox.ModAPI;
using VRageMath;
using VRage.Groups;
using VRage.Input;
using Sandbox.Game;
using VRage;
using VRage.Game;
using VRage.Game.Entity;
using Sandbox.Definitions;
using Sandbox.Game.Multiplayer;
using VRage.Game.ModAPI;

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
