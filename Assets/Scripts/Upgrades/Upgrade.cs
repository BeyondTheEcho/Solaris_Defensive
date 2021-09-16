using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Upgrades
{
    public abstract class Upgrade<T> : Upgrade
    {
        
        public T Value => Active.Value;
        public new UpgradeTier<T> Active => Tiers[ActiveIndex];
        public new UpgradeTier<T> Next => ActiveIndex == Tiers.Count - 1 ? null : Tiers[ActiveIndex + 1];

        public override List<UpgradeTier> GetTiers() => Tiers.Cast<UpgradeTier>().ToList();
        public List<UpgradeTier<T>> Tiers;
    }

    public abstract class Upgrade : ScriptableObject
    {
        public string Name;
        public string Key;
        public abstract List<UpgradeTier> GetTiers();
        public UpgradeTier Active => GetTiers()[ActiveIndex];
        public UpgradeTier Next => ActiveIndex == GetTiers().Count - 1 ? null : GetTiers()[ActiveIndex + 1];
        
        public int ActiveIndex
        {
            get => PlayerPrefs.GetInt(Key, 0);
            set
            {
                #if UNITY_EDITOR
                if (value < 0 || value >= GetTiers().Count)
                    UnityEngine.Debug.LogError($"Tried setting upgrade {Name} to level {value}");
                else
                #endif
                    PlayerPrefs.SetInt(Key, value);
            }
        }
        
        #if UNITY_EDITOR
        public void Debug()
        {
            if (ActiveIndex == GetTiers().Count - 1) ActiveIndex = 0;
            else ActiveIndex++;
        }
        #endif
    }
}

