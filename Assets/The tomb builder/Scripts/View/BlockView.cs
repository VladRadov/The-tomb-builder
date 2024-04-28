using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockView : MonoBehaviour
{
    [SerializeField] private Vector3 _stepIncreaseScale;
    [SerializeField] private float _speedIncreaseScale;
    [SerializeField] private Color _colorDrop;
    [Header("Components")]
    [SerializeField] private Transform _center;
    [SerializeField] private Image _image;
    [SerializeField] private Rigidbody2D _rigidbody;

    public Rigidbody2D BaseRigidbody => _rigidbody;
    public Transform Center => _center;
    public Vector3 StepIncreaseScale => _stepIncreaseScale;
    public float SpeedIncreaseScale => _speedIncreaseScale;

    public void UpdatePosition(Vector2 newPosition)
        => transform.localPosition = newPosition;

    public void UpdateScale(Vector3 scale)
        => transform.localScale = scale;

    public void StartCoroutineDropBlock()
        => StartCoroutine(DropBlock());

    public IEnumerator DropBlock()
    {
        _image.color = _colorDrop;
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

    private void OnValidate()
    {
        if (_image == null)
            _image = GetComponent<Image>();

        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody2D>();
    }
}
