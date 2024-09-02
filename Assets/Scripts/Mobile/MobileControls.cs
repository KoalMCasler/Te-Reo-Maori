using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileControls : MonoBehaviour
{
    public void OpenMobileKeyboard()
    {
        TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
    }
}
