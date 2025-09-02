using UnityEngine;

public class FShootState : IState
{
    private EnemyData data;
    StateMachine currentState;
    GameObject[] bullet;

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
            data.shootTimer = data.timeBetweenShoot;
           
        }

    }

    public void FixedUpdate()
    {
        
    }

    public void Exit()
    {
        
    }
    public GameObject GetBullet()
    {
        for (int i = 0; i < bullet.Length; i++)
        {
            if (!bullet[i].activeInHierarchy)
            {
                return bullet[i];
            }
        }
        return null; // no free bullet
    }

    

}
