using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableIO", menuName = "Scriptables")]
public class ScriptableIO : ScriptableObject
{
    public bool IsPickedUp;
    public bool IsActivated;
    public bool PickupCheck()
    {
        return IsPickedUp;
    }
}