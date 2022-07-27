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
    public class Plugin : IPlugin, IDisposable
    {
        public void Dispose()
        {
           

        }

        public void Init(object gameInstance)
        {
         
            Reflections.Initialize();
            
        }

        public void Update()
        {

            Tether.Update();    
            
        }


    }
}


