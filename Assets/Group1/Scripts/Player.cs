using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _baseSpeed;
    
    private void Update()
    {
        var offset = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.Translate(offset * _baseSpeed * Time.deltaTime);
    }

    private IEnumerator SpeedBooster(float bonusTimeLeft, float speedMultiplier)
    {
        var waitForSeconds = new WaitForSeconds(bonusTimeLeft);

        _baseSpeed *= speedMultiplier;
        yield return waitForSeconds;
        _baseSpeed /= speedMultiplier;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            enemy.Die();
        }

        if (collision.TryGetComponent(out SpeedBonus bonus))
        {
            StartCoroutine(SpeedBooster(bonus.BonusTime, bonus.SpeedMultiplier));
            Destroy(bonus.gameObject);
        }
    }
}

