using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private Player _player;
    [SerializeField] private List<Enemy> _enemies;
    [SerializeField] private List<SpeedBonus> _bonuses;
    [SerializeField] private float _collisionDistance = 0.2f;

    private void OnEnable()
    {
        foreach (var enemy in _enemies)
            enemy.Dead += OnDead;
    }

    private void OnDisable()
    {
        foreach (var enemy in _enemies)
            enemy.Dead -= OnDead;
    }

    private void Update()
    {
        CollisionDetector();
    }

    private void CollisionDetector()
    {
        foreach (var enemy in _enemies)
        {
            if (Vector3.Distance(_player.gameObject.transform.position, enemy.gameObject.transform.position) < _collisionDistance)
            {
                enemy.Die();
            }
        }
        foreach (var bonus in _bonuses)
        {
            if (Vector3.Distance(_player.gameObject.transform.position, bonus.gameObject.transform.position) < _collisionDistance)
            {
                _player.IncreaseSpeed();
            }
        }
    }

    private void OnDead()
    {
        var countAliveEnemy = _enemies.Count(enemy => enemy.ISAlive == true);
        if (countAliveEnemy == 0)
        {
            _gameOverScreen.SetActive(true);
        }
    }
}
