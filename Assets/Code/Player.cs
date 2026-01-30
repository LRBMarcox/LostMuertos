using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour {
    public float speed = 4f;
    private Rigidbody2D rb;
    private Vector2 input;
    private Torch _torch;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _torch = GetComponentInChildren<Torch>();
    }

    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        input = input.normalized;

        if (_torch != null) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePos - _torch.transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _torch.transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = input * speed;

    }
}
