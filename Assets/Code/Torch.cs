using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Torch : MonoBehaviour {
    public float minIntensity = 0.8f;
    public float maxIntensity = 3f;
    public float flickerSpeed = 0.05f;
    private Light2D light2D;
    private float battery;
    private float tempBattery;
    const float maxBattery = 100f;
    private float tempMinIntensity, tempMaxIntensity;
    public float batteryDrowningSpeed = 1.0f;

    void Awake()
    {
        light2D = GetComponent<Light2D>();
        tempMinIntensity = minIntensity;
        tempMaxIntensity = maxIntensity;
        battery = maxBattery;
        tempBattery = maxBattery;
}

    void Update()
    {
        tempMinIntensity = minIntensity * (battery / maxBattery);
        tempMaxIntensity = maxIntensity * (battery / maxBattery);

        light2D.intensity = Mathf.Lerp(light2D.intensity,
                                       Random.Range(tempMinIntensity, tempMaxIntensity),
                                       flickerSpeed);

        tempBattery -= Time.deltaTime * batteryDrowningSpeed;

        if (battery - tempBattery >= 10f) {
            battery = tempBattery;
        }
        Debug.Log("battery = " + battery.ToString());
    }
}