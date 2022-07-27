using System;
using Sandbox.ModAPI;
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
            
            if(!message.StartsWith("/tether", StringComparison.OrdinalIgnoreCase))
            {         
                return;
            }

            sendToOthers = false;

            GetTargetedBlock.GetPort();


        }


    }
}
