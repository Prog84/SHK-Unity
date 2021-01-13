using UnityEngine;
using UnityEngine.Events;

public class Enemy: MonoBehaviour
{
    [SerializeField] private int _radiusValue = 4;
    [SerializeField] private float _speed = 2;

    private Vector3 _target;
    private bool _isAlive = true;

    public bool ISAlive => _isAlive;

    public event UnityAction Dead;

    private void Start()
    {
        NextTarget();
    }

    private void Update()
    {
        Move();

        if (transform.position == _target)
        {
            NextTarget();
        }

    }

    private void NextTarget()
    {
        _target = Random.insideUnitCircle * _radiusValue;
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);
    }

    public void Die()
    {
        _isAlive = false;
        gameObject.SetActive(false);
        Dead?.Invoke();
    }
}

