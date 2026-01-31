using UnityEngine;

public class SpiritAI : MonoBehaviour {
    public float wanderSpeed = 1.2f;
    public float fleeSpeed = 2f;
    public float changeDirTime = 3f;

    protected Rigidbody2D rb;
    protected Vector2 moveDir;
    protected bool isFleeing;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        PickRandomDirection();
        InvokeRepeating(nameof(PickRandomDirection), changeDirTime, changeDirTime);
    }

    protected virtual void FixedUpdate()
    {
        float speed = isFleeing ? fleeSpeed : wanderSpeed;
        rb.linearVelocity = moveDir * speed;
    }

    protected void PickRandomDirection()
    {
        if (isFleeing) return;
        moveDir = Random.insideUnitCircle.normalized;
    }

    // chiamata dalla torcia
    public virtual void OnIlluminated(Vector2 lightPos)
    {
        isFleeing = true;
        moveDir = (rb.position - lightPos).normalized;
        CancelInvoke();
        Invoke(nameof(StopFlee), 0.8f);
    }

    protected void StopFlee()
    {
        isFleeing = false;
        PickRandomDirection();
        InvokeRepeating(nameof(PickRandomDirection), changeDirTime, changeDirTime);
    }
}
