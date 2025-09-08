using UnityEngine;

public interface IState
{
    void Enter();           // Called when state is entered
    void Update();          // Called every frame
    void FixedUpdate();     // Called every physics frame (optional)
    void Exit();            // Called when switching out of state

    void OnTriggerEnter2D(Collider2D other) { }  // Optional: Handle trigger enter events
    void OnTriggerExit2D(Collider2D other) { }   // Optional: Handle trigger exit events
}
