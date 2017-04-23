using Duality;
using Duality.Resources;

namespace PuddleJumper.Core.Helpers
{
    public static class MenuHelpers
    {
        public static GameObject GetPlaneInMenuPrefab(int i)
        {
            var menuGameObject = Scene.Current.FindGameObject("Menu").ChildByName("Planes");
            return menuGameObject.ChildByName("PlaneInMenu" + i);
        }
    }
}