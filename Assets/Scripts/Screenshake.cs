using UnityEngine;

public class Screenshake : MonoBehaviour
{
    float Duration = 0f;
    float MagnitudeX = 0f;
    float MagnitudeY = 0f;
    Vector3 Displace = Vector3.zero;

    private static Screenshake _instance;
    
    public void Awake()
    {
        _instance = this;
    }

    public void Update()
    {
        if (Duration <= 0f)
            return;

        Vector3 old = transform.localPosition - Displace;
        float scale = Duration < 0.3f ? Duration / 0.3f : 1f;
        Displace = new Vector3(scale * (Random.value - 0.5f) * 2f * MagnitudeX, scale * (Random.value - 0.5f) * 2f * MagnitudeY, 0f);

        transform.localPosition = old + Displace;

        Duration -= Time.deltaTime;
        if (Duration <= 0f) {
            transform.localPosition = old;
            Displace = Vector3.zero;
            MagnitudeX = 0f;
            MagnitudeY = 0f;
        }
    }

    public static void Shake(float magnitude, float duration)
    {
        if (_instance == null)
            return;

        _instance.MagnitudeX = magnitude > _instance.MagnitudeX ? magnitude : _instance.MagnitudeX;
        _instance.MagnitudeY = magnitude > _instance.MagnitudeY ? magnitude : _instance.MagnitudeY;
        _instance.Duration = duration > _instance.Duration ? duration : _instance.Duration;
    }

    public static void Shake(float magX, float magY, float duration)
    {
        if (_instance == null)
            return;

        _instance.MagnitudeX = magX > _instance.MagnitudeX ? magX : _instance.MagnitudeX;
        _instance.MagnitudeY = magY > _instance.MagnitudeY ? magY : _instance.MagnitudeY;
        _instance.Duration = duration > _instance.Duration ? duration : _instance.Duration;
    }
}