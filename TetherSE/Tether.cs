using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Sandbox.Game;
using Sandbox.Game.Entities;
using Sandbox.Game.Entities.Character;
using Sandbox.Game.World;
using Sandbox.ModAPI;
using VRage.Game.Entity;
using VRage.Game.ModAPI;
using Sandbox.Game.Entities.Inventory;
using Sandbox.Game.Weapons;
using VRage.Game;
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

            if (ticks < 50)
            {
                ticks++;
                return;
            }

            ticks = 0;


            if (GetTargetedBlock.selectedBlock == null)
            {
                return;
            }

            if (Vector3D.Distance(localPlayer.PositionComp.GetPosition(),
                    GetTargetedBlock.selectedBlock.GetPosition()) > Patches.maxUseDistance)
            {
                utils.ShowMessage("Tether Broke!",
                    $"You Moved More Than {Patches.maxUseDistance}m from tethered block.");
                GetTargetedBlock.selectedBlock = null;
                GetTargetedBlock.selectedObject = null;
                return;
            }

            var equippedTool = MySession.Static.LocalCharacter.HandItemDefinition.Id.SubtypeName;
            if (equippedTool.Contains("Welder", StringComparison.OrdinalIgnoreCase))
            {
                if (localPlayer.BuildPlanner.Count == 0)
                {
                    return;
                }
                DoWelder(localPlayer);
                return;
            }
            if (equippedTool.Contains("Grinder", StringComparison.OrdinalIgnoreCase))
            {
                DoGrinder();
                return;
            }
            if (equippedTool.Contains("Drill", StringComparison.OrdinalIgnoreCase))
            {
                DoDrill();
                return;
            }
        }

        public static void DoWelder(MyCharacter localPlayer)
        {
            Patches.UseObjectPatch(Patches.maxUseDistance);
            Reflections.Withdraw.Invoke(null, new object[] { (MyEntity)GetTargetedBlock.selectedObject.Owner, localPlayer.GetInventory(0), null });
            Patches.UseObjectPatch(5f);
            return;
        }

        public static void DoGrinder()
        {
            var inventory = (MyInventory)GetTargetedBlock.selectedBlock.GetInventory();
            foreach (var objectId in MySession.Static.LocalCharacter.GetInventory().GetItems())
            {
                if (!objectId.Content.GetObjectId().ToString().ToLower().Contains("ore") &&
                    !objectId.Content.GetObjectId().ToString().ToLower().Contains("ingot") &&
                    !objectId.Content.GetObjectId().ToString().ToLower().Contains("component")) continue;
                MyConstants.DEFAULT_INTERACTIVE_DISTANCE = 10000;
                MyInventory.TransferByPlanner(MySession.Static.LocalCharacter.GetInventory(), inventory, objectId.Content.GetObjectId(), MyItemFlags.None, objectId.Amount);
                MyConstants.DEFAULT_INTERACTIVE_DISTANCE = 10;
            }
        }
        public static void DoDrill()
        {
            var inventory = (MyInventory)GetTargetedBlock.selectedBlock.GetInventory();
            foreach (var objectId in MySession.Static.LocalCharacter.GetInventory().GetItems())
            {
                if (!objectId.Content.GetObjectId().ToString().ToLower().Contains("ore")) continue;
                MyConstants.DEFAULT_INTERACTIVE_DISTANCE = 10000;
                MyInventory.TransferByPlanner(MySession.Static.LocalCharacter.GetInventory(), inventory, objectId.Content.GetObjectId(), MyItemFlags.None, objectId.Amount);
                MyConstants.DEFAULT_INTERACTIVE_DISTANCE = 10;
            }
        }
        public static int ticks = 0;
    }
}
