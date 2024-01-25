using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinalWindow : MonoBehaviour
{
    [SerializeField] private TMP_Text resultMark;

    public void ShowWindow(int result)
    {
        gameObject.SetActive(true);
        resultMark.text = $"{result}/100";
    }
}
