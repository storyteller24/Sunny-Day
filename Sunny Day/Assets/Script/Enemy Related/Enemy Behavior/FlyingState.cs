using UnityEngine;

public class FlyingState : IState
{
    private EnemyData data;
    float direction;
    StateMachine currentState;
    private float directionX;
    private float directionY;
    
    public FlyingState(EnemyData Data, StateMachine State)
    {
        data = Data;
        currentState = State;
    }
    public void Enter()
    {
        data.anim.Play("Fly");
    }

    public void Update()
    {
        Vector2 targetPos = data.target.transform.position;
        Vector2 myPos = data.transform.position;

       
        Vector2 diff = targetPos - myPos;

        // Normalize to get only direction
        Vector2 dir = diff.normalized;
        directionX = dir.x;
        directionY = dir.y;


    }

    public void FixedUpdate()
    {
            Vector2 velocity = new Vector2(directionX * data.speed, directionY * data.speed);
            data.rb.linearVelocity = velocity;
        if (directionX != 0)
        {
            data.transform.localScale = new Vector3(Mathf.Sign(directionX), 1, 1);
        }
    }

    public void Exit()
    {

    }

    


}
