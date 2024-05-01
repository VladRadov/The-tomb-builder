using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataTomb", menuName = "ScriptableObject/DataTomb")]
public class DataTomb : ScriptableObject
{
    [SerializeField] private Vector2 _positionFirstBlock;
    [SerializeField] private float _minDistanceEqualBlock;

    public Vector2 PositionFirstBlock => _positionFirstBlock;
    public float MinDistanceEqualBlock => _minDistanceEqualBlock;
}
