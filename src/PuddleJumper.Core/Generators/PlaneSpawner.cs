using System;
using Duality;
using Duality.Resources;
using PuddleJumper.Core.GameObjects;

namespace PuddleJumper.Core.Generators
{
    public class PlaneSpawner
    {
        private ContentRef<Prefab> airportPrefab = null;
        private Lazy<World> lazyWorld = new Lazy<World>(() => Startup.World);
        private World world { get { return lazyWorld.Value; } }

        private Random rng = new Random();
    }
}