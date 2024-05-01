using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Tomb
{
    private List<Block> _blocks;
    private Vector2 _positionFirstBlock;
    private bool _isIncreaseScaleLastBlock;
    private bool _isTombBuildEnd;

    public Tomb(Vector2 positionFirstBlock)
    {
        _blocks = new List<Block>();
        _positionFirstBlock = positionFirstBlock;
        _isIncreaseScaleLastBlock = false;
        _isTombBuildEnd = false;
    }

    public int CountBlocks => _blocks.Count;
    public Block LastBlock => _blocks.Count != 0 ? _blocks[_blocks.Count - 1] : new BlockNull();
    public bool IsIncreaseScaleLastBlock => _isIncreaseScaleLastBlock;

    public void OnEndTombBuilded()
        => _isTombBuildEnd = true;

    public void BuildTomb(Block block)
    {
        if (_blocks.Count == 0)
            block.UpdatePosition(_positionFirstBlock);

        _blocks.Add(block);
    }

    public void DeleteLastBlock()
        => _blocks.Remove(LastBlock);

    public void SetIsIncreaseScaleLastBlock(bool value)
        => _isIncreaseScaleLastBlock = value;

    public void IncreaseScaleLastBlock()
    {
        if(_isTombBuildEnd == false && _isIncreaseScaleLastBlock)
            LastBlock.IncreaseScale();
    }
}
