
using UnityEngine;


public class PatrolState : IState
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private EnemyData data;
    float direction;
    private StateMachine currentState;
   
    public PatrolState(EnemyData patrolData, StateMachine state )
    {
        data = patrolData;
        currentState = state;
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
        data.rb.linearVelocity = new Vector2(data.speed * direction, data.rb.linearVelocity.y);
    }

    public void Exit()
    {
        
    }
    private void Flip()
    {
        float distance = Vector2.Distance(data.transform.position, data.spawnPoint);
        float directionToSpawn = Mathf.Sign(data.spawnPoint.x - data.transform.position.x);
        if (distance > data.maxDistance || data.isSomethingAhead)
        {
            direction = directionToSpawn;
            Vector3 scale = data.transform.localScale;
            scale.x = direction;
            data.transform.localScale = scale;       
           
        }
    }
    
}
