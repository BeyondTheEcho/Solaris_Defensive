using System.Collections.Generic;

namespace Upgrades
{
    [System.Serializable]
    public class Upgrades
    {
        public IntUpgrade LaserDamage;
        public FloatUpgrade TestUpgrade2;
        public FloatArrUpgrade TestUpgrade3;

        public Upgrade[] UpgradeArray => new Upgrade[]
        {
            LaserDamage,
            TestUpgrade2,
            TestUpgrade3
        };
    }
}