using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SystemInput _systemInput;
    [SerializeField] private SpawnerController _spawnerController;
    [SerializeField] private DataTomb _dataTomb;
    [SerializeField] private TimelineManager _timelineManager;
    [SerializeField] private HealthManager _healthManager;
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private CameraMove _cameraMove;

    private Tomb _tomb;

    private TombController _tombController;

    private void Start()
    {
        _tomb = new Tomb(_dataTomb.PositionFirstBlock);
        _tombController = new TombController(_tomb);

        Subscriber();
        AddObjectsDisposable();
    }

    private void Subscriber()
    {
        _timelineManager.OnTimerEndCommand.Subscribe(_ => { _timelineManager.RestartTimer(); });
        _timelineManager.OnTimerEndCommand.Subscribe(_ => { _tombController.OnEndTimer(); });
        _timelineManager.OnTimerEndCommand.Subscribe(_ => { _healthManager.Damage(); });
        _systemInput.OnMouseDownCommand.Subscribe( _ => { _spawnerController.SpawnBlock(_tomb.LastBlock); } );
        _systemInput.OnMouseDownCommand.Subscribe( _ => { _cameraMove.SetTarget(); /*if(_tomb.LastBlock is Block) _cameraMove.Move(_tomb.LastBlock.Position.Value);*/ } );
        _systemInput.OnMouseDownCommand.Subscribe( _ => { _tomb.SetIsIncreaseScaleLastBlock(true); } );
        _systemInput.OnMouseUpCommand.Subscribe( _ => { _tomb.SetIsIncreaseScaleLastBlock(false); } );
        _systemInput.OnMouseUpCommand.Subscribe( _ => { _timelineManager.RestartTimer(); } );
        _systemInput.OnMouseUpCommand.Subscribe( _ => { if(_tombController.IsBlockBuild) _scoreManager.AddScore(); } );
        _spawnerController.OnSpawnBlockComand.Subscribe((block) => { _tombController.AddBlockToTomb(block); });
    }

    private void AddObjectsDisposable()
    {
        ManagerUniRx.AddObjectDisposable(_systemInput.OnMouseDownCommand);
        ManagerUniRx.AddObjectDisposable(_systemInput.OnMouseUpCommand);
        ManagerUniRx.AddObjectDisposable(_spawnerController.OnSpawnBlockComand);
        ManagerUniRx.AddObjectDisposable(_timelineManager.OnTimerEndCommand);
        ManagerUniRx.AddObjectDisposable(_healthManager.GameOverCommand);
    }

    private void FixedUpdate()
    {
        _tombController.BaseUpdate();
        _timelineManager.UpdateTimer();
    }
}
