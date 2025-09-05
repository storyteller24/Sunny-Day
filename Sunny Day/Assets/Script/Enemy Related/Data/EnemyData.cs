
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
  
    public GameObject target;
   
    public bool isGround;
    public bool isSomethingAhead;

    [Header("Shoot Data")]
    public List<GameObject> projectiles;
    public Transform shootPoint;





}
