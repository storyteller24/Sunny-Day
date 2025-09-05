using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Stats")]
public class EnemyStats : ScriptableObject
{
    [Header("Enemy Stats")]
    public float speed;
    public float maxDistance;
    public float attackRange;
    public int health;

    [Header("Shooting Stats")]
    public int numberOfBullets;
    public float projectileSpeed;
    public float shootCooldown;
    public float timeBetweenBurst;
    public GameObject projectilePrefab;
}