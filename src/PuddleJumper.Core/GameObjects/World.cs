using System.Collections.Generic;
using System.Linq;
using Duality;
using Duality.Input;
using Duality.Resources;
using PuddleJumper.Core.GameObjects.Map;
using PuddleJumper.Core.GameObjects.Plane;
using PuddleJumper.Core.Generators;
using PuddleJumper.Core.Shapes;

namespace PuddleJumper.Core.GameObjects
{
    public class World
    {
        private readonly Scorekeeper scorekeeper;
        public WorldMap MapObject { get; set; }
        public WorldMapData MapData { get; set; }
        public AirportSpawner AirportSpawner { get; set; }
        public PlaneSpawner PlaneSpawner { get; set; }
        public List<AirportController> Airports { get; set; } = new List<AirportController>();
        public List<PlaneController> Planes { get; set; } = new List<PlaneController>();
        public PlaneController SelectedPlane { get; set; } = null;

        private ContentRef<Prefab> planeInMenuPrefab = null;

        private bool regenerateWorld;

        public World(WorldMapData mapData, AirportSpawner airportSpawner, PlaneSpawner planeSpawner, Scorekeeper scorekeeper)
        {
            this.scorekeeper = scorekeeper;
            regenerateWorld = true;
            MapData = mapData;
            AirportSpawner = airportSpawner;
            PlaneSpawner = planeSpawner;
        }

        public void RegenerateWorld()
        {
            regenerateWorld = true;
        }

        private bool hasInit = false;
        private void Init()
        {
            if (DualityApp.ExecContext == DualityApp.ExecutionContext.Editor) return;

            if (planeInMenuPrefab == null)
            {
                planeInMenuPrefab = ContentProvider.RequestContent<Prefab>(@"Data\Prefabs\PlaneInMenu.Prefab.res");
            }

            var menuGameObject = Scene.Current.FindGameObject("Menu").ChildByName("Planes");

            for (int i = 0; i < 4; i++)
            {
                var i1 = i;

                var planeMenuObj = planeInMenuPrefab.Res.Instantiate();
                planeMenuObj.Parent = menuGameObject;
                planeMenuObj.Transform.RelativePos = new Vector3(0, i * 214, -10);
                planeMenuObj.Name = "PlaneInMenu" + (i + 1);
                planeMenuObj.ChildByName("RightArea").ChildByName("HasPlane").Active = false;
                planeMenuObj.ChildByName("LeftArea").ChildByName("Image").Active = false;
                foreach (var buyPlaneChild in planeMenuObj.ChildByName("RightArea").ChildByName("NoPlane").Children.Where(child => child.Name == "BuyPlane"))
                {
                    var button = buyPlaneChild.ChildByName("Button").GetComponent<Button>();
                    switch (button.Name)
                    {
                        case "BuyPlane11":
                            button.ClickAction = () => TryBuyPlane(PlaneTypes.PuddleJumper, i1 + 1);
                            break;
                        case "BuyPlane12":
                            button.ClickAction = () => TryBuyPlane(PlaneTypes.DualProp, i1 + 1);
                            break;
                        case "BuyPlane13":
                            button.ClickAction = () => TryBuyPlane(PlaneTypes.NarrowBody, i1 + 1);
                            break;
                        case "BuyPlane14":
                            button.ClickAction = () => TryBuyPlane(PlaneTypes.Heavy, i1 + 1);
                            break;
                    }
                }

                var sellButton = planeMenuObj.ChildByName("RightArea").ChildByName("HasPlane").ChildByName("SellButton").ChildByName("Background").GetComponent<Button>();
                sellButton.ClickAction = () =>
                {
                    var plane = Planes.SingleOrDefault(p => p.Number == i1 + 1);
                    if (plane != null)
                    {
                        int value = plane.Type.GetValue() / 2;
                        scorekeeper.Money += value;

                        plane.Destroy();
                        Planes.Remove(plane);
                    }
                };
            }

            hasInit = true;
        }

        private void TryBuyPlane(PlaneTypes type, int number = 0)
        {
            int amount = type.GetValue();

            if (scorekeeper.Money >= amount)
            {
                scorekeeper.Money -= amount;
                PlaneSpawner.SpawnPlane(type, number);
            }
        }

        private bool firstPlaneSpawned = false;
        public void Update()
        {
            if (!hasInit)
            {
                Init();
            }

            if (regenerateWorld)
            {
                if(MapObject == null)
                    MapObject = Scene.Current.FindComponent<WorldMap>();

                if (MapObject != null)
                {
                    GenerateNewMap();
                    regenerateWorld = false;
                }
            }

            if (DualityApp.ExecContext == DualityApp.ExecutionContext.Editor)
            {
                return;
            }

            AirportSpawner.Update();
            if (!firstPlaneSpawned)
            {
                firstPlaneSpawned = true;
                //PlaneSpawner.SpawnPlane(PlaneTypes.PuddleJumper);
                //PlaneSpawner.SpawnPlane(PlaneTypes.DualProp);
                PlaneSpawner.SpawnPlane(PlaneTypes.NarrowBody);
                //PlaneSpawner.SpawnPlane(PlaneTypes.Heavy);
                //PlaneSpawner.SpawnPlane(PlaneTypes.DualProp);
            }

            if (DualityApp.Keyboard.KeyHit(Key.Escape) && SelectedPlane != null)
            {
                SelectedPlane.IsSelected = false;
                SelectedPlane = null;
            }

            scorekeeper.UpdateUi();
        }

        public void GenerateNewMap()
        {
            MapData.Regenerate();
            MapObject.Draw(MapData);
        }
    }
}