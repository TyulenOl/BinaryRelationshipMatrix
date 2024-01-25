using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private int minSetSize = 2;
    [SerializeField] private int maxSetSize = 5;
    
    [SerializeField] private int minSetValue = 1;
    [SerializeField] private int maxSetValue = 10;

    private void Awake()
    {
        var randomMatrix =
            BinaryRelationshipMatrix.CreateRandomBinaryMatrix(minSetSize, maxSetSize, minSetValue, maxSetValue);
        
        Debug.Log(randomMatrix.GetRelationshipString());
    }
}
