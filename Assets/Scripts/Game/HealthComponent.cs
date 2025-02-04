using UnityEngine;
using UnityEngine.UI;

public abstract class HealthComponent : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _currentHealth;
    [SerializeField] private GameObject _healthBar;
    [SerializeField] private Transform _healthFill;
    public int CurrentHealth => _currentHealth;
    public int MaxHealth => _maxHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _currentHealth = Mathf.Max(_currentHealth, 0);
        Debug.Log((_currentHealth / _maxHealth) / 5);
        _healthFill.localScale = new Vector3(((float)_currentHealth/ (float)_maxHealth)/5, _healthFill.localScale.y, _healthFill.localScale.z);
        
        if (_currentHealth <= 0)
        {
            Debug.Log(this);
            HandleDeath();
        }
    }

    public abstract void HandleDeath();
}