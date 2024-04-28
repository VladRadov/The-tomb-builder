using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataTomb", menuName = "ScriptableObject/DataTomb")]
public class DataTomb : ScriptableObject
{
    [SerializeField] private Vector2 _positionFirstBlock;

    public Vector2 PositionFirstBlock => _positionFirstBlock;
}
