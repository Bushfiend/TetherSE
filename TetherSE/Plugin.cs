using System;
using HarmonyLib;
using VRage.Plugins;


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


