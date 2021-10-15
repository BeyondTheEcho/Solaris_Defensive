using UnityEngine;

public class DamageSender : MonoBehaviour
{
    public int Damage;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        var player = other.GetComponent<Player>();
        player.TakeDamage(Damage);
    }
}
