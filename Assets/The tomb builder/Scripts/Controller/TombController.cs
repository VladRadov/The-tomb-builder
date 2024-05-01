using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class TombController
{
    private Tomb _tomb;
    private BlockView _lastBlockView;
    private bool _isBlockBuild;
    private int _countBlocksGame;

    public ReactiveCommand OnTombDestroyCommand = new ();
    public ReactiveCommand OnWinGame = new();

    public TombController(Tomb tomb)
    {
        _tomb = tomb;
    }

    public void SetCountBlocksGame(int value)
        => _countBlocksGame = value;

    public bool IsBlockBuild => _isBlockBuild;

    public void AddBlockToTomb(BlockView blockView)
    {
        _isBlockBuild = true;
        _lastBlockView = blockView;
        blockView.OnBlockDownCommand.Subscribe(_ => { OnTombDestroyCommand.Execute(); });

        var block = new Block();
        block.Center = blockView.Center;
        block.UpdatePosition(blockView.transform.position);
        block.SetSpeedIncreaseScale(blockView.SpeedIncreaseScale);
        block.SetStepIncreaseScale(blockView.StepIncreaseScale);
        block.Position.Subscribe((value) => { blockView.UpdatePosition(value); });
        block.Scale.Subscribe((value) => { blockView.UpdateScale(value); });

        _tomb.BuildTomb(block);
        AudioManager.Instance.PlayBuildBlock();
    }

    public bool TryBuildBlockTop()
        => _tomb.CountBlocks == _countBlocksGame - 1;

    public void CheckingFinishLevel()
    {
        if (_countBlocksGame == _tomb.CountBlocks)
        {
            OnWinGame.Execute();
            _tomb.OnEndTombBuilded();

            if(ContainerSaveerPlayerPrefs.Instance.SaveerData.EndLevels.Contains(ContainerSaveerPlayerPrefs.Instance.SaveerData.Level.ToString()) == false)
                ContainerSaveerPlayerPrefs.Instance.SaveerData.EndLevels += ContainerSaveerPlayerPrefs.Instance.SaveerData.Level + ";";
        }
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
