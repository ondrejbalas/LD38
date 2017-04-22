using Duality;
using Duality.Resources;
using Ninject;
using PuddleJumper.Core.GameObjects;
using PuddleJumper.Core.GameObjects.Map;

namespace PuddleJumper.Core
{
    public class Startup : CorePlugin
    {
        private bool initComplete = false;

        private IKernel kernel;
        public static World World { get; private set; }

        protected override void InitPlugin()
        {
            base.InitPlugin();

            Configure();

            var scene = ContentProvider.RequestContent<Scene>(MagicStrings.MainScene);
            scene.MakeAvailable();

            World = kernel.Get<World>();

            initComplete = true;
        }

        private void Configure()
        {
            kernel = new StandardKernel();
            kernel.Bind<World>().ToSelf().InSingletonScope();
            kernel.Bind<WorldMapData>().ToSelf().InSingletonScope();
        }

        private bool IsCalled = false;
        protected override void OnBeforeUpdate()
        {
            base.OnBeforeUpdate();
            if (!initComplete) return;

            IsCalled = true;

            World.Update();
        }
    }
}