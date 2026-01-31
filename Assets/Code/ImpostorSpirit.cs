using UnityEngine;

public class ImpostorSpirit : SpiritAI {
    public Sprite normalSprite;
    public Sprite skeletonSprite;
    private SpriteRenderer sr;

    protected override void Awake()
    {
        base.Awake();
        sr = GetComponent<SpriteRenderer>();
    }

    public override void OnIlluminated(Vector2 lightPos)
    {
        base.OnIlluminated(lightPos);
        StartCoroutine(SkeletonFlash());
    }

    System.Collections.IEnumerator SkeletonFlash()
    {
        sr.sprite = skeletonSprite;
        yield return null; // frame 1
        yield return null; // frame 2
        sr.sprite = normalSprite;
    }
}
