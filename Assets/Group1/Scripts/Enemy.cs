using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _movementRadius = 4;
    [SerializeField] private float _speed = 2;

    private Vector3 _target;

    public event UnityAction<Enemy> Dead;

    private void Start()
    {
        NextTarget();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);

        if (transform.position == _target)
        {
            NextTarget();
        }

    }

    private void NextTarget()
    {
        _target = Random.insideUnitCircle * _movementRadius;
    }

    public void Die()
    {
        Dead?.Invoke(this);
        Destroy(gameObject);
    }
}
