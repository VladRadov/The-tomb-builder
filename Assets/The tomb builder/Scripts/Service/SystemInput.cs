using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class SystemInput : MonoBehaviour
{
    public ReactiveCommand OnMouseDownCommand = new ();
    public ReactiveCommand OnMouseUpCommand = new ();

    private void OnMouseDown()
    {
        OnMouseDownCommand.Execute();
    }

    private void OnMouseUp()
    {
        OnMouseUpCommand.Execute();
    }

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(OnMouseDownCommand);
        ManagerUniRx.Dispose(OnMouseUpCommand);
    }
}
