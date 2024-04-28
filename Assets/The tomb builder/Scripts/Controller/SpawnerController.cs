using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class SpawnerController : MonoBehaviour
{
    [SerializeField] private BlockView _prefabBlock;
    [SerializeField] private Transform _parent;

    public ReactiveCommand<BlockView> OnSpawnBlockComand = new();

    public void SpawnBlock(Block parent)
    {
        var block = Instantiate(_prefabBlock, parent is BlockNull? _parent : parent.Center);
        OnSpawnBlockComand.Execute(block);
    }

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(OnSpawnBlockComand);
    }
}
