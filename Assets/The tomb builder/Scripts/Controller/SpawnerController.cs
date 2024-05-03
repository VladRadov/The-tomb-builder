using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class SpawnerController : MonoBehaviour
{
    [SerializeField] private BlockView _prefabBlock;
    [SerializeField] private BlockView _prefabBlockTop;
    [SerializeField] private Transform _parent;

    public ReactiveCommand<BlockView> OnSpawnBlockComand = new();

    public void SpawnBlock(Block parent, BlockView blockView, bool isBuildBlockTop)
    {
        //var block = Instantiate(isBuildBlockTop ? _prefabBlockTop : _prefabBlock, parent is BlockNull ? _parent : parent.Center);

        var block = Instantiate(isBuildBlockTop ? _prefabBlockTop : _prefabBlock, _parent);
        if (blockView != null)
        {
            var newPosition = new Vector2(blockView.transform.localPosition.x, blockView.transform.localPosition.y + blockView.BoxCollider.size.y);
            block.UpdatePositionWorld(newPosition);
        }

        OnSpawnBlockComand.Execute(block);
    }

    public void SetSpriteBlock(Sprite sprite)
        => _prefabBlock.SetSpriteBlock(sprite);

    public void SetSpriteBlockTop(Sprite sprite)
        => _prefabBlockTop.SetSpriteBlock(sprite);

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(OnSpawnBlockComand);
    }
}
