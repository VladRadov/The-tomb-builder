using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Vector3 _target;

    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _step;

    public void SetTarget()
        => _target = transform.position + _step;

    private void Move()
    {
        var newPosition = Vector3.Lerp(transform.position, _target, _speed);
        transform.position = new Vector3(transform.position.x, newPosition.y, transform.position.z);
    }

    private void FixedUpdate()
    {
        if(Vector3.Distance(transform.position, _target) > 0.1)
            Move();
    }
}
