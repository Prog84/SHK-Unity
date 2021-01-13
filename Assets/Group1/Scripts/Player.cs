using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private bool _timer;
    [SerializeField] private float _time;

    private float _defaultSpeed;
    private float _maxSpeed;
    private float _timeBonusSpeed;

    private void Start()
    {
        _maxSpeed = _speed * 2;
        _defaultSpeed = _speed;
        _timeBonusSpeed = _time;
    }

    private void Update()
    {
        ReduceSpeed();

        var offset = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.Translate(offset * _speed * Time.deltaTime);
    }

    public void IncreaseSpeed()
    {
        _speed = _maxSpeed;
        _timer = true;
        _time += _timeBonusSpeed;
    }

    private void ReduceSpeed()
    {
        if (_timer)
        {
            _time -= Time.deltaTime;
            if (_time < 0)
            {
                _timer = false;
                _speed = _defaultSpeed;
            }
        }
    }
}
