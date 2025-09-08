
using UnityEngine;


public class PatrolState : IState
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private EnemyData data;
    private EnemyStats stats;
    float direction;
    private StateMachine currentState;
   
    public PatrolState(EnemyData patrolData, StateMachine state, EnemyStats stats )
    {
        data = patrolData;
        currentState = state;
        this.stats = stats;
    }

    public void Enter()
    {
        data.anim.Play("Walk");  
        direction = data.transform.localScale.x;
    }

    public void Update()
    {
        Flip();        
    }

    public void FixedUpdate() 
    {
        data.rb.linearVelocity = new Vector2(stats.speed * direction, data.rb.linearVelocity.y);
    }

    public void Exit()
    {
        
    }

    private void Flip()
    {
        float distance = Vector2.Distance(data.transform.position, data.spawnPoint);
        float directionToSpawn = Mathf.Sign(data.spawnPoint.x - data.transform.position.x);
        if (distance > stats.maxDistance || data.isSomethingAhead)
        {
            direction = directionToSpawn;
            Vector3 scale = data.transform.localScale;
            scale.x = direction;
            data.transform.localScale = scale;                  
        }
    }
    
}
