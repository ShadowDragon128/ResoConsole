using Elements.Core;
using FrooxEngine;
using FrooxEngine.UIX;

namespace ResoConsole
{
    internal class ConsolePanel
    {
        Slot workSlot;
        // Sync<T> values seem to not be initialized, therefore not usable in game until otherwise
        public ValueField<bool> showWindow;
        public ValueField<string> title;

        public ConsolePanel(Slot slot, ConsoleController consoleController)
        {
            workSlot = slot;
            workSlot.PersistentSelf = false;
            workSlot.LocalScale *= 0.0005f;

            Slot dataSlot = workSlot.AddSlot("DataSlot");
            showWindow = dataSlot.AttachComponent<ValueField<bool>>();
            title = dataSlot.AttachComponent<ValueField<string>>();

            showWindow.Value.Value = true;
            title.Value.Value = "ResoConsole";

            showWindow.Changed += delegate (IChangeable c)
            {
                WinAPI.ShowWindow(consoleController.ConsoleHandle, showWindow.Value ? WinAPI.SW_SHOW : WinAPI.SW_HIDE);
            };
            title.Changed += delegate (IChangeable c)
            {
                WinAPI.SetWindowText(consoleController.ConsoleHandle, title.Value);
            };


            UIBuilder canvasUI = RadiantUI_Panel.SetupPanel(workSlot, "Console", new float2(540, 1080));
            RadiantUI_Constants.SetupEditorStyle(canvasUI);

            VerticalLayout vlayout = canvasUI.VerticalLayout(4f, childAlignment: Alignment.TopCenter);
            vlayout.ForceExpandHeight.Value = false;


            canvasUI.Style.MinHeight = 24f;
            canvasUI.Style.MinWidth = 400f;
            canvasUI.Style.PreferredHeight = 24f;
            canvasUI.Style.PreferredWidth = 400f;

            UniLog.Log("workSlot: " + workSlot.ToString());
            UniLog.Log("consoleController: " + consoleController.ToString());
            UniLog.Log("canvasUI1: " + canvasUI.Current);

            canvasUI.HorizontalElementWithLabel("Console Handle: ", .35f, () => canvasUI.Text(consoleController.ConsoleHandle.ToString("0x16"), alignment: Alignment.MiddleCenter));
            canvasUI.HorizontalElementWithLabel("Show Window:", .35f, () => BooleanMemberEditor(canvasUI, showWindow.Value));
            UniLog.Log("canvasUI2: " + canvasUI.Current);

            //Slot f = BuildField(title.Value, "Title", canvasUI);
            //canvasUI.HorizontalElementWithLabel("Title:", .35f, () => PrimitiveMemberEditor(canvasUI, title.Value));
            //UniLog.Log("canvasUI3: " + canvasUI.Current);

            workSlot.PositionInFrontOfUser(float3.Backward, distance: 1);
        }

        private BooleanMemberEditor BooleanMemberEditor(UIBuilder ui, IField field, string path = null)
        {
            ui.PushStyle();

            ui.Style.MinWidth = -1f;
            ui.Style.PreferredWidth = -1f;
            ui.Style.FlexibleWidth = -1f;
            ui.Style.MinHeight = -1f;
            ui.Style.PreferredHeight = -1f;
            ui.Style.FlexibleHeight = -1f;

            BooleanMemberEditor f = UIBuilderEditors.BooleanMemberEditor(ui, field, path);

            ui.PopStyle();
            ui.NestOut();

            return f;
        }
    }
}
