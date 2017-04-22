using Duality;
using Duality.Components;
using Duality.Components.Renderers;

namespace PuddleJumper.Core.GameObjects
{
    [RequiredComponent(typeof(TextRenderer))]
    [RequiredComponent(typeof(Transform))]
    public class Airport : Component, ICmpInitializable
    {
        public string Name { get; set; }
        public int Size { get; set; }

        public Airport(string name)
        {
            Name = name;
        }

        public void OnInit(InitContext context)
        {
            throw new System.NotImplementedException();
        }

        public void OnShutdown(ShutdownContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}