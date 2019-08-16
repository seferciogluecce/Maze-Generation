using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInfoManager : MonoBehaviour
{
    public Text HeightValue;
    public Text WidthValue;
    public void UpdateUI(int height, int width)
    {
        HeightValue.text = height.ToString();
        WidthValue.text = width.ToString();
    }   
}
