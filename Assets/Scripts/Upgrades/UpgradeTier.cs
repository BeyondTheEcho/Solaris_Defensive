using UnityEngine;

namespace Upgrades
{
    public class UpgradeTier<T> : UpgradeTier
    {
        public T Value;
    }

    public class UpgradeTier : ScriptableObject
    {
        public string Name;
        [TextArea]
        public string Description;
        public int Cost;
    }
}