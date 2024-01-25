using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private Image bgImage;
    [SerializeField] private Button button;
    
    [SerializeField] private Sprite oneSprite;
    [SerializeField] private Sprite zeroSprite;

    public bool Value { get; private set; }

    private void Awake()
    {
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        Value = !Value;
        if (Value)
        {
            text.text = "1";
            text.color = Color.white;
            bgImage.sprite = oneSprite;
        }
        else
        {
            text.text = "0";
            text.color = Color.black;
            bgImage.sprite = zeroSprite;
        }
    }
}
