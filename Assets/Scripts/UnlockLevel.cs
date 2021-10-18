using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockLevel : MonoBehaviour
{
    public int Level;
    
    public void Unlock()
    {
        Stats.LevelUnlocked = Mathf.Max(Stats.LevelUnlocked, Level);
        Debug.Log($"Level unlocked: {Stats.LevelUnlocked}");
    }
}
