using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private List<GameObject> _collisionObjects;

    private int _countEnemies = 0;

    private void OnEnable()
    {
        foreach (var collisionObject in _collisionObjects)
        {
            if (collisionObject.TryGetComponent(out Enemy enemy))
            {
                enemy.Dead += OnDead;
                _countEnemies++;
            }
        }
    }

    private void OnDisable()
    {
        foreach (var collisionObject in _collisionObjects)
        {
            if (collisionObject.TryGetComponent(out Enemy enemy))
            {
                enemy.Dead -= OnDead;
            }
        }
    }

    private void OnDead(Enemy enemy)
    {
        enemy.Dead -= OnDead;
        _countEnemies--;
        _collisionObjects.Remove(enemy.gameObject);

        if (_countEnemies == 0)
        {
            _gameOverScreen.SetActive(true);
        }
    }
}
