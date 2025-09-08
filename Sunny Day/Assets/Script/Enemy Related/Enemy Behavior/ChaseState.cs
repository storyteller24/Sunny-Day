using UnityEngine;

public class ChaseState : IState
{
    private EnemyData data;
    private EnemyStats stats;
    float direction;
    StateMachine currentState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public ChaseState(EnemyData chaseData, StateMachine State, EnemyStats stats)
    {
        data = chaseData;
        currentState = State;
        this.stats = stats;
    }
    public void Enter()
    {
        data.anim.Play("Walk");        
    }

    public void Update()
    {       
        direction = Mathf.Sign(data.target.transform.position.x - data.transform.position.x);
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
        Vector3 scale = data.transform.localScale;
        scale.x = direction;
        data.transform.localScale = scale;
    }
}
