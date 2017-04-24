namespace PuddleJumper.Core.GameObjects.Plane
{
    public enum PlaneTypes
    {
        PuddleJumper = 1,
        DualProp = 2,
        NarrowBody = 3,
        Heavy = 4
    }

    public static class PlaneTypesHelpers
    {
        public static int GetValue(this PlaneTypes type)
        {
            switch (type)
            {
                case PlaneTypes.PuddleJumper:
                    return 10000;
                case PlaneTypes.DualProp:
                    return 25000;
                case PlaneTypes.NarrowBody:
                    return 60000;
                case PlaneTypes.Heavy:
                    return 150000;
                default:
                    return 999999;
            }
        }
    }
}