using UnityEngine;

public class FShootState : IState
{
    private EnemyData data;
    private EnemyStats stats;
    StateMachine currentState;
    private float timer = 3f;

    public FShootState(EnemyData shootData, StateMachine state, EnemyStats stats)
    {
        data = shootData;
        currentState = state;
        this.stats = stats;
    }
    public void Enter()
    {
        data.rb.linearVelocity = Vector2.zero;
        data.anim.Play("Flying");       
    }

    public void Update()
    {
        timer -= Time.deltaTime;
        GameObject bullet = ObjectPool.Instance.GetPooledObject(data.projectiles);
        if (timer <= 0f)
        {
            timer = stats.shootCooldown;
            if (bullet != null)
            {
                bullet.transform.position = data.shootPoint.position;
                bullet.SetActive(true);
                Vector2 direction = (data.target.transform.position - data.shootPoint.position).normalized;
                bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * stats.projectileSpeed;
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




