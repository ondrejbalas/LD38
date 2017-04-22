using Duality;
using Duality.Resources;

namespace PuddleJumper.Core
{
    public class Startup : CorePlugin
    {
        protected override void InitPlugin()
        {
            base.InitPlugin();

            var scene = ContentProvider.RequestContent<Scene>(MagicStrings.MainScene);
            scene.MakeAvailable();
        }

        protected override void OnBeforeUpdate()
        {
            base.OnBeforeUpdate();
        }
    }
}