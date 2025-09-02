using System;
using System.Collections;
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
    public GameObject projectilePrefab;
    public int numberOfBullets;

    public float projectileSpeed;
    public float timeBetweenShoot;
    public float timeBetweenBurst;
    [HideInInspector] public float shootTimer;




}
