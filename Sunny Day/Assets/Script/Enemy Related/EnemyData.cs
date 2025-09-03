using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyData
{
    [Header("Enemy Stats")]
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Animator anim;
    [HideInInspector] public Vector2 spawnPoint;
    [HideInInspector] public Transform transform;
    [HideInInspector] public RaycastHit2D hit;
    public float speed;
    public float maxDistance;
    public GameObject target;
    public float attackRange;
    public bool isGround;
    public bool isSomethingAhead;

    [Header("Shoot Data")]
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public List<GameObject> projectiles;
    public int numberOfBullets;

    public float projectileSpeed;
    public float shootCooldown;
    public float timeBetweenBurst;
    [HideInInspector] public float shootTimer;




}
