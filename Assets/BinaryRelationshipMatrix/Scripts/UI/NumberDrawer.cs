using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumberDrawer : MonoBehaviour
{
    [SerializeField] private TMP_Text numberText;

    public void ReDraw(int number)
    {
        numberText.text = number.ToString();
    }
}
