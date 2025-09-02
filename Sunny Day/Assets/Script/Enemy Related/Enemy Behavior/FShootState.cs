using UnityEngine;

public class FShootState : IState
{
    private EnemyData data;
    StateMachine currentState;
    private int bulletsRemainingInBurst;
    private float burstTimer;

    public FShootState(EnemyData shootData, StateMachine state)
    {
        data = shootData;
        currentState = state;
    }
    public void Enter()
    {
        data.rb.linearVelocity = Vector2.zero;
        data.anim.Play("Flying");       
    }

    public void Update()
    {
        data.shootTimer -= Time.deltaTime;
        if (data.shootTimer <= 0f)
        {           
            data.shootTimer = data.shootCooldown;           
        }
    }

    public void FixedUpdate()
    {
        
    }

    public void Exit()
    {
        
    }  
}




