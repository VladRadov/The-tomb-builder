using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class TombController
{
    private Tomb _tomb;
    private BlockView _lastBlockView;
    private bool _isBlockBuild;

    public TombController(Tomb tomb)
    {
        _tomb = tomb;
    }

    public bool IsBlockBuild => _isBlockBuild;

    public void AddBlockToTomb(BlockView blockView)
    {
        _isBlockBuild = true;
        _lastBlockView = blockView;

        var block = new Block();
        block.Center = blockView.Center;
        block.UpdatePosition(blockView.transform.position);
        block.SetSpeedIncreaseScale(blockView.SpeedIncreaseScale);
        block.SetStepIncreaseScale(blockView.StepIncreaseScale);
        block.Position.Subscribe((value) => { blockView.UpdatePosition(value); });
        block.Scale.Subscribe((value) => { blockView.UpdateScale(value); });

        _tomb.BuildTomb(block);
    }

    public void OnEndTimer()
    {
        if (_tomb.IsIncreaseScaleLastBlock)
        {
            _isBlockBuild = false;
            _tomb.DeleteLastBlock();
            _lastBlockView.StartCoroutineDropBlock();
        }
    }

    public void BaseUpdate()
    {
        _tomb.IncreaseScaleLastBlock();
    }
}
