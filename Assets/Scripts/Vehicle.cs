using UnityEngine;

public class Vehicle : MonoBehaviour
{
    [SerializeField] private Collider2D _destroyer;

    public void SetDestroyer(Collider2D collider)
    {
        _destroyer = collider;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_destroyer == collision.collider)
            Destroy(gameObject);
    }
}
