using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private Player _player;
    [SerializeField] private List<GameObject> _collisionObjects;

    private int _countEnimies = 0;

    private void OnEnable()
    {
        foreach (var collisionObject in _collisionObjects)
        {
            if (collisionObject.TryGetComponent(out Enemy enemy))
            {
                enemy.Dead += OnDead;
                _countEnimies++;
            }
        }
    }

    private void OnDead(Enemy enemy)
    {
        enemy.Dead -= OnDead;
        _countEnimies--;
        _collisionObjects.Remove(enemy.gameObject);

        if (_countEnimies == 0)
        {
            _gameOverScreen.SetActive(true);
        }
    }
}
