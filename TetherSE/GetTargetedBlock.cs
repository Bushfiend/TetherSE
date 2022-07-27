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
using Sandbox.ModAPI.Weapons;
using Sandbox.Game.Weapons;

namespace TetherSE
{
    public class GetTargetedBlock
    {
        public static void GetPort()
        {
            IMyUtilities utils = MyAPIGateway.Utilities;      
            MySlimBlock block;
            var localplayer = MySession.Static.LocalHumanPlayer;

            var caster = MyAPIGateway.Session.Player.Character?.EquippedTool?.Components?.Get<MyCasterComponent>();
            block = caster?.HitBlock;

            if(selectedBlock != null)
            {
                MyVisualScriptLogicProvider.SetHighlightLocal(selectedBlock.Name, -1, 0, Color.White, MySession.Static.LocalPlayerId, null);
            }
 
            
            if(block != null && block.FatBlock is IMyTerminalBlock)
            {
                var terminalBlock = block.FatBlock as IMyTerminalBlock;
                var useObjectBase = terminalBlock.Components.Get<MyUseObjectsComponentBase>();

                List<IMyUseObject> useableObjects = new List<IMyUseObject>();
                useObjectBase.GetInteractiveObjects(useableObjects);

                if(localplayer.GetRelationTo(terminalBlock.OwnerId) != MyRelationsBetweenPlayerAndBlock.Owner)
                {
                    utils.ShowNotification("You do not own this block... Nice try.", 5000, "Red");
                    return;
                }

                foreach (var useableObject in useableObjects)
                {
                    if (useableObject.PrimaryAction == UseActionEnum.OpenInventory)
                    {
                        utils.ShowNotification($"Tethered to {terminalBlock.CustomName}", 5000, "White");
                        selectedBlock = terminalBlock;
                        selectedObject = useableObject;

                        terminalBlock.Name = terminalBlock.EntityId.ToString();
                        MyEntities.SetEntityName((MyEntity)terminalBlock);
                        MyVisualScriptLogicProvider.SetHighlightLocal(terminalBlock.Name, 2, 3, Color.ForestGreen, MySession.Static.LocalPlayerId, null);

                        return;
                    }
                }
            }

            utils.ShowNotification("Block doesn't exist or does not have a inventory access port.", 5000, "Red");   
        }

        public static IMyUseObject selectedObject;
        public static IMyTerminalBlock selectedBlock;
    }
}
