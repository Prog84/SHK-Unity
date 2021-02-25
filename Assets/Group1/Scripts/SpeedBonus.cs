using UnityEngine;

public class SpeedBonus : MonoBehaviour
{
    [SerializeField] private float _speedMultiplier;
    [SerializeField] private float _bonusTime;

    public float SpeedMultiplier => _speedMultiplier;
    public float BonusTime => _bonusTime;
}
