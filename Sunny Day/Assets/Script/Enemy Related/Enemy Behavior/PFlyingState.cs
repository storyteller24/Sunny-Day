using System.Collections;
using UnityEngine;


// A behaviour that is attached to a playable
public class PFlyingState : IState
{
    private EnemyData data;
    private EnemyStats stats;

    // Called when the owning graph starts playing

    StateMachine currentState;
    private Vector2 direction;
    private Vector2 patrolVelocity;    
    private float patrolTimer = 0f;

    public PFlyingState(EnemyData chaseData, StateMachine state, EnemyStats stats)
    {
        data = chaseData;
        currentState = state;
        this.stats = stats;
    }
    public void Enter()
    {
        data.anim.Play("Fly");        
    }

    public void Update()
    {
        patrolTimer -= Time.deltaTime;
        if (patrolTimer <= 0f)
        {
            patrolTimer = 3f; // reset timer
            Vector2 randomPoint = GetRandomPoint(data.spawnPoint, stats.maxDistance);
            direction = (randomPoint - (Vector2)data.transform.position).normalized;
           
            patrolVelocity = direction * stats.speed;
        }
    }

    public void FixedUpdate()
    {
        data.rb.linearVelocity = patrolVelocity;
        data.transform.localScale = new Vector3(Mathf.Sign(direction.x), 1, 1);
    }

    public void Exit()
    {

    }

    Vector2 GetRandomPoint(Vector2 center, float radius)
    {
        // Random.insideUnitCircle gives a random point inside a circle with radius = 1
        Vector2 randomOffset = Random.insideUnitCircle * radius;
        return center + randomOffset;
    }

}
