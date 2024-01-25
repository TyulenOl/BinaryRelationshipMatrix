using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    [SerializeField] private int minSetSize = 2;
    [SerializeField] private int maxSetSize = 4;
    
    [SerializeField] private int minSetValue = 2;
    [SerializeField] private int maxSetValue = 4;

    [SerializeField] private MatrixDrawer matrixDrawer;
    [SerializeField] private Button checkButton;
    [SerializeField] private FinalWindow finalWindow;
    
    private BinaryRelationshipMatrix answerMatrix;
    
    private void Awake()
    {
        checkButton.onClick.AddListener(OnClick);
        answerMatrix = BinaryRelationshipMatrix.CreateRandomBinaryMatrix(minSetSize, maxSetSize, minSetValue, maxSetValue);
        matrixDrawer.Init(answerMatrix);
    }

    private void OnClick()
    {
        var check = answerMatrix.CheckMatrix(matrixDrawer.GetMatrix());
        finalWindow.ShowWindow(check ? 100 : 0);
    }
}
