using HarmonyLib;
using ResoniteModLoader;

namespace ResoConsole
{
    public class ResoConsole : ResoniteMod
    {
        /*
            As this will be a console mod I wonder if I should use the asynchronous (multi-threaded)
            code from my personal FileEngine library. Maybe when I get back.

            Notes:
            1.) Support both ResoniteModLoader and MoneyLoader
         */

        internal readonly ConsoleController consoleController = new ConsoleController(true); // Start during initialization

        public override string Name => "ResoConsole";

        public override string Author => "ShadowAPI";

        public override string Version => "1.0.0";

        public override string Link => "";

        public override void OnEngineInit()
        {
            //Harmony.DEBUG = true;
            
            base.OnEngineInit();


            //Engine.Current.RunPostInit(delegate ()
            //{
            //    DevCreateNewForm.AddAction("Editor", "Console Output (Experimental)", delegate (Slot slot) { new ConsolePanel(slot, consoleController); });
            //});

            Harmony rHarmony = new Harmony("usmx.ShadowAPI.ResoConsole");

            Patches.ResoniteModLoader_Logger.CheckAndPatch(rHarmony);
        }
    }
}
