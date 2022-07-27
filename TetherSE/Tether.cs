using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Sandbox.Game.Entities;
using Sandbox.Game.World;
using Sandbox.ModAPI;
using VRage.Game.Entity;
using VRage.Game.ModAPI;
using Sandbox.Game.Entities.Inventory;
using VRageMath;


namespace TetherSE
{
    class Tether
    {

        public static void Update()
        {
            if (MyAPIGateway.Multiplayer == null || MySession.Static.LocalCharacter == null)
            {
                return;
            }

            var localPlayer = MySession.Static.LocalCharacter;
            IMyUtilities utils = MyAPIGateway.Utilities;

            if (localPlayer.BuildPlanner.Count == 0)
            {
                return;
            }

            if (ticks < 50)
            {
                ticks++;
                return;
            }
            ticks = 0;

            if (GetTargetedBlock.selectedBlock != null)
            {
                
                if (Vector3D.Distance(localPlayer.PositionComp.GetPosition(), GetTargetedBlock.selectedBlock.GetPosition()) < Patches.maxUseDistance)
                {
                    ticks = 0;
                    Patches.UseObjectPatch(Patches.maxUseDistance);
                    Reflections.Withdraw.Invoke(null, new object[] { (MyEntity)GetTargetedBlock.selectedObject.Owner, localPlayer.GetInventory(0), null });
                    Patches.UseObjectPatch(5f);
                    return;

                } else
                {

                    utils.ShowMessage("Tether Broke!", $"You Moved More Than {Patches.maxUseDistance}m from tethered block.");
                    GetTargetedBlock.selectedBlock = null;
                    GetTargetedBlock.selectedObject = null;
                    return; 
                }
            }     
        }

        public static int ticks = 0;
    }
}
