using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Sandbox.Game;
using Sandbox.Game.Entities;
using Sandbox.Game.World;
using Sandbox.ModAPI;
using VRage;
using VRage.Game;
using VRage.Game.Entity;
using VRage.Groups;
using Sandbox.Definitions;
using Sandbox.Game.Multiplayer;
using HarmonyLib;
using VRage.Plugins;
using VRage.Utils;
using VRage.Input;
using VRage.Game.ModAPI;
using VRage.ModAPI;
using Sandbox.Common.ObjectBuilders.Definitions;
using Sandbox.Engine.Multiplayer;
using Sandbox.Engine.Utils;
using Sandbox.Game.Components;
using Sandbox.Game.Entities.Character;
using Sandbox.Game.Entities.Character.Components;
using Sandbox.Game.Entities.Cube;
using Sandbox.Game.Entities.Interfaces;
using Sandbox.Game.Entities.Inventory;
using Sandbox.Game.EntityComponents;
using Sandbox.Game.GameSystems;
using Sandbox.Game.GameSystems.Conveyors;
using Sandbox.Game.Gui;
using Sandbox.Game.GUI;
using Sandbox.Game.Replication;
using VRage.Audio;
using VRage.Game.Components;
using VRage.Game.Entity.UseObject;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ObjectBuilders.ComponentSystem;
using VRage.Library.Collections;
using VRage.Library.Utils;
using VRage.Network;
using VRage.ObjectBuilders;
using VRage.Sync;
using VRageMath;
using Sandbox.Game.Localization;

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
