using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _startSpeed;
    [SerializeField] private bool _timer;
    [SerializeField] private float _timeBonusSpeed;

    private float _currentSpeed;
    private float _speedMultiplier;
    private float _bonusTimeLeft;

    private void Start()
    {
        _speedMultiplier = 2;
        _currentSpeed = _startSpeed;
    }

    private void Update()
    {
        ReduceSpeed();

        var offset = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.Translate(offset * _currentSpeed * Time.deltaTime);
    }

    public void IncreaseSpeed()
    {
        _timer = true;
        if (_currentSpeed == _startSpeed * _speedMultiplier)
        {
            _bonusTimeLeft += _timeBonusSpeed;
        }
        else
        {
            _currentSpeed *= _speedMultiplier;
            _bonusTimeLeft = _timeBonusSpeed;
        }
    }

    private void ReduceSpeed()
    {
        if (_timer)
        {
            _bonusTimeLeft -= Time.deltaTime;
            if (_bonusTimeLeft < 0)
            {
                _timer = false;
                _currentSpeed /= _speedMultiplier;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            enemy.Die();
        }

        if (collision.TryGetComponent(out SpeedBonus bonus))
        {
            IncreaseSpeed();
            Destroy(bonus.gameObject);
        }
    }
}

