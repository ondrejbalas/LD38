using Duality;
using Duality.Resources;
using Ninject;
using PuddleJumper.Core.GameObjects;

namespace PuddleJumper.Core
{
    public class Startup : CorePlugin
    {
        private IKernel kernel;
        private World world;

        protected override void InitPlugin()
        {
            base.InitPlugin();
            Configure();

            var scene = ContentProvider.RequestContent<Scene>(MagicStrings.MainScene);
            scene.MakeAvailable();

            world = kernel.Get<World>();
        }

        private void Configure()
        {
            kernel = new StandardKernel();
            kernel.Bind<World>().ToSelf().InSingletonScope();
        }

        protected override void OnBeforeUpdate()
        {
            base.OnBeforeUpdate();
            
            world.Update();
        }
    }
}