using System;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Windows;

public class InputFilter : MonoBehaviour
{
    public TMP_Text textDisplay;
    public TMP_InputField inputText;

    public TextAsset textAssetBlockList;
    [SerializeField] string[] strBlockList;
    public void Start()
    {
        strBlockList = textAssetBlockList.text.Split(new string[] { ",", "\n" }, StringSplitOptions.RemoveEmptyEntries);
    }

    public void CheckInput()
    {
        textDisplay.text = ProfanityCheck(inputText.text);
        //Debug.Log(textDisplay.text);
        //Debug.Log(inputText.text);
    }

    private string ProfanityCheck(string strToCheck)
    {
        for (int i = 0; i < strBlockList.Length; i++)
        {
            // Create a regular expression to match whole words
            Regex regex = new Regex(@"\b" + Regex.Escape(strBlockList[i]) + @"\b", RegexOptions.IgnoreCase);

            // Replace any occurrences of the banned word with asterisks
            strToCheck = regex.Replace(strToCheck, "***");
        }
        return strToCheck;
    }
}
