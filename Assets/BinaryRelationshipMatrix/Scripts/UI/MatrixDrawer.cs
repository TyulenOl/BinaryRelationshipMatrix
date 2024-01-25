using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MatrixDrawer : MonoBehaviour
{
    [SerializeField] private NumberDrawer columnDrawerPrefab;
    [SerializeField] private NumberDrawer rowDrawerPrefab;
    [SerializeField] private AnswerButton answerButtonPrefab;

    [SerializeField] private LayoutGroup columnsLayoutGroup;
    [SerializeField] private LayoutGroup rowsLayoutGroup;
    [SerializeField] private GridLayoutGroup buttonsGrid;

    [SerializeField] private TMP_Text stringText;

    private int[] Rows;
    private int[] Columns;
    
    private AnswerButton[,] answerButtons;

    public void Init(BinaryRelationshipMatrix matrix)
    {
        Rows = matrix.Rows;
        Columns = matrix.Columns;
        
        
        stringText.text = matrix.GetRelationshipString();

        foreach (var value in Rows)
        {
            var instance = Instantiate(rowDrawerPrefab, rowsLayoutGroup.transform);
            instance.ReDraw(value);
        }

        foreach (var value in Columns)
        {
            var instance = Instantiate(columnDrawerPrefab, columnsLayoutGroup.transform);
            instance.ReDraw(value);
        }

        buttonsGrid.cellSize = GetCellSize(Rows.Length, Columns.Length);

        answerButtons = new AnswerButton[Rows.Length,Columns.Length];//[Row,Col]

        for (var row = 0; row < Rows.Length; row++)
        {
            for (var col = 0; col < Columns.Length; col++)
            {
                var instance = Instantiate(answerButtonPrefab, buttonsGrid.transform);
                answerButtons[row, col] = instance;
            }
        }
    }

    public BinaryRelationshipMatrix GetMatrix()
    {
        var rows = answerButtons.GetUpperBound(0) + 1;    // количество строк
        var columns = answerButtons.Length / rows;
        var dict = new Dictionary<(int, int), bool>();
        for (var row = 0; row < rows; row++)
        {
            for (var col = 0; col < columns; col++)
            {
                dict[(Rows[row], Columns[col])] = answerButtons[row, col].Value;
            }
        }

        return new BinaryRelationshipMatrix(dict);
    }

    private Vector2 GetCellSize(int rowSize, int colSize)
    {
        var rect = buttonsGrid.GetComponent<RectTransform>().rect;
        var width = (rect.width - buttonsGrid.spacing.x * (colSize - 1)) / colSize;
        var height = (rect.height - buttonsGrid.spacing.y * (rowSize - 1)) / rowSize;
        return new Vector2(width, height);
    }
}
