using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour {
    public float speed = 4f;
    private Rigidbody2D rb;
    private Vector2 input;
    private Torch _torch;
    public float torchRange = 3f;
    public LayerMask npcLayer;
    public float captureRange = 1.2f;


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

        DetectSpirits();

        if (Input.GetKeyDown(KeyCode.E))
            TryCapture();
    }


    void FixedUpdate()
    {
        rb.linearVelocity = input * speed;

    }

    void DetectSpirits()
    {
        Vector2 origin = transform.position;
        Vector2 dir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;

        RaycastHit2D[] hits = Physics2D.CircleCastAll(
            origin + dir * 1f,
            0.8f,
            dir,
            torchRange,
            npcLayer
        );

        foreach (var hit in hits) {
            SpiritAI spirit = hit.collider.GetComponent<SpiritAI>();
            if (spirit != null)
                spirit.OnIlluminated(origin);
        }
    }

    void TryCapture()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position,
            captureRange,
            npcLayer
        );

        foreach (var hit in hits) {
            ImpostorSpirit impostor = hit.GetComponent<ImpostorSpirit>();
            if (impostor != null) {
                Destroy(impostor.gameObject);
                break;
            }
        }
    }
}
