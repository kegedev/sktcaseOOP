using UnityEngine;

public class CollectableHealth : MonoBehaviour, IInteractable
{
    [SerializeField] private int _healthBonus = 10;

    public void Interact(SnakeSegment segment)
    {
        Destroy(gameObject);

        Debug.Log($"Collectible collected! Health increased by {_healthBonus}.");
    }
}