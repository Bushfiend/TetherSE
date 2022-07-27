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
using VRage.Game.Components;

namespace TetherSE
{
    [MySessionComponentDescriptor(MyUpdateOrder.NoUpdate)]
    public class Commands : MySessionComponentBase
    {
        public override void BeforeStart()
        {
            MyAPIUtilities.Static.MessageEntered += Command;
        }
        protected override void UnloadData()
        {
            MyAPIUtilities.Static.MessageEntered -= Command;
            base.UnloadData();
        }

        private static void Command(string message, ref bool sendToOthers)
        {
            sendToOthers = false;

            if(message.StartsWith("/tether", StringComparison.OrdinalIgnoreCase))
            {
                GetTargetedBlock.GetPort();
            }

        }


    }
}
