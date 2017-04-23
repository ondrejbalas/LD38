using SnowyPeak.Duality.Plugins.YAUI;
using SnowyPeak.Duality.Plugins.YAUI.Controls;

namespace PuddleJumper.Core
{
    public class Menu : UI
    {
        protected override ControlsContainer CreateUI()
        {
            var canvas = new CanvasPanel();

            var text = new TextBlock();
            text.Text = "You have some money";

            canvas.Add(text);

            return canvas;
        }
    }
}