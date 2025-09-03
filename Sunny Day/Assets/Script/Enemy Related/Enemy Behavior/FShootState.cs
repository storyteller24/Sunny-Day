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
        GameObject bullet = ObjectPool.Instance.GetPooledObject(data.projectiles);
        if (data.shootTimer <= 0f)
        {
            data.shootTimer = data.shootCooldown;
            if (bullet != null)
            {
                bullet.transform.position = data.shootPoint.position;
                bullet.SetActive(true);
                Vector2 direction = (data.target.transform.position - data.shootPoint.position).normalized;
                bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * data.projectileSpeed;
            }
        }
    }

    public void FixedUpdate()
    {
        
    }

    public void Exit()
    {
        
    }  
}




