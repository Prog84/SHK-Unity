using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _baseSpeed;
    [SerializeField] private float _timeBonusSpeed;

    private float _speedMultiplier;
    private float _bonusTimeLeft;
    private int _countBonuses;

    private void Start()
    {
        _countBonuses = 0;
        _speedMultiplier = 2;       
    }

    private void Update()
    {
        ReduceSpeed();

        var offset = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.Translate(offset * _baseSpeed * Time.deltaTime);
    }

    public void IncreaseSpeed()
    {
        _countBonuses++;
        _baseSpeed *= _speedMultiplier;
        _bonusTimeLeft = _timeBonusSpeed;
    }

    private void ReduceSpeed()
    {
        if (_countBonuses > 0)
        {
            _bonusTimeLeft -= Time.deltaTime;

            if (_bonusTimeLeft < 0)
            {
                _countBonuses--;
                _baseSpeed /= _speedMultiplier;

                if (_countBonuses > 0)
                {
                    _bonusTimeLeft = _timeBonusSpeed;
                }
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

