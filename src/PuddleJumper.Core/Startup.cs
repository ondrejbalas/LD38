using Duality;
using Duality.Resources;
using Ninject;
using PuddleJumper.Core.GameObjects;
using PuddleJumper.Core.GameObjects.Map;
using PuddleJumper.Core.Helpers;

namespace PuddleJumper.Core
{
    public class Startup : CorePlugin
    {
        private bool initComplete = false;

        private IKernel kernel;
        public static World World { get; private set; }

        protected override void InitPlugin()
        {
            DualityUserData data = DualityApp.UserData;
            data.GfxMode = ScreenMode.Fullscreen;
            data.GfxWidth = 1600;
            data.GfxHeight = 900;
            DualityApp.UserData = data;
            DualityApp.SaveUserData();

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
        
        private bool addedObject = false;
        protected override void OnBeforeUpdate()
        {
            base.OnBeforeUpdate();
            if (!initComplete) return;

            if (!addedObject)
            {
                var go = Scene.Current.FindGameObject("WorldMap");
                if (go != null)
                {
                    go.AddComponent<WorldMap>();
                    addedObject = true;
                }
            }

            if(DualityApp.ExecContext != DualityApp.ExecutionContext.Editor)
                ScalingHelpers.SetScale();

            World.Update();
        }
    }
}