using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Random = UnityEngine.Random;

public class BinaryRelationshipMatrix
{
    private readonly int[] rowSet;
    private readonly int[] columnSet;
    
    private readonly Dictionary<(int, int), bool> matrix;

    public BinaryRelationshipMatrix(int[] rowSet, int[] columnSet)
    {
        this.rowSet = rowSet;
        this.columnSet = columnSet;

        matrix = new Dictionary<(int, int), bool>();
        
        foreach (var row in rowSet)
        foreach (var column in columnSet)
            matrix[(row, column)] = false;
    }

    public BinaryRelationshipMatrix(Dictionary<(int, int), bool> matrix)
    {
        this.matrix = new Dictionary<(int, int), bool>(matrix);

        var rowValues = new HashSet<int>();
        var columnValues = new HashSet<int>();
        foreach (var (row, column) in matrix.Keys)
        {
            rowValues.Add(row);
            columnValues.Add(column);
        }

        rowSet = rowValues.OrderBy(value => value).ToArray();
        columnSet = columnValues.OrderBy(value => value).ToArray();
    }

    public void AddRelation(int row, int column)
    {
        if (!rowSet.Contains(row) || !columnSet.Contains(column))
            throw new IndexOutOfRangeException();

        matrix[(row, column)] = true;
    }
    
    public void RemoveRelation(int row, int column)
    {
        if (!rowSet.Contains(row) || !columnSet.Contains(column))
            throw new IndexOutOfRangeException();

        matrix[(row, column)] = false;
    }

    public string GetRelationshipString()
    {
        var relationshipString = GetRelationship().Select((pair) => $"({pair.Item1};{pair.Item2})");
        
        return $"R = {{{string.Join(", ", relationshipString)}}}";
    }

    public IEnumerable<(int, int)> GetRelationship()
    {
        return matrix.Keys.Where(pair => matrix[pair]).ToArray();
    }

    public bool CheckMatrix(BinaryRelationshipMatrix verifiableMatrix)
    {
        if (rowSet.Length != verifiableMatrix.rowSet.Length ||
            columnSet.Length != verifiableMatrix.columnSet.Length) return false;
        
        if (rowSet.Where((value, index) => value != verifiableMatrix.rowSet[index]).Any())
            return false;
        
        return !columnSet.Where((value, index) => value != verifiableMatrix.columnSet[index]).Any();
    }
    
    public static BinaryRelationshipMatrix CreateRandomBinaryMatrix(int minSize, int maxSize, int minSetValue, int maxSetValue)
    {
        var firstRandomSet = CreateRandomSet(minSize, maxSize, minSetValue, maxSetValue).ToArray();
        var secondRandomSet = CreateRandomSet(minSize, maxSize, minSetValue, maxSetValue).ToArray();

        var randomMatrix = new Dictionary<(int, int), bool>();

        foreach (var row in firstRandomSet)
        foreach (var column in secondRandomSet)
            randomMatrix[(row, column)] = Random.Range(0, 2) == 1;

        return new BinaryRelationshipMatrix(randomMatrix);
    }

    private static IEnumerable<int> CreateRandomSet(int minSize, int maxSize, int minSetValue, int maxSetValue)
    {
        var relationSize = Random.Range(minSize, maxSize);

        if (relationSize > maxSetValue - minSetValue + 1)
            throw new ArgumentException("The size of the relation is greater than the possible number of values");
        
        var randomValues = new HashSet<int> ();
        while (randomValues.Count != relationSize)
            randomValues.Add(Random.Range(minSetValue, maxSetValue));

        return randomValues.OrderBy(value => value).ToArray();
    }
}
