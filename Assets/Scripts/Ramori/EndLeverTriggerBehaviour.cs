using UnityEngine;

public class EndLeverTriggerBehaviour : MonoBehaviour
{
    static Transform redBot;

    public float TriggerDistance = 1f;

    void Start()
    {
        if (redBot == null)
            redBot = GameObject.Find("RedBot").transform;
    }

    void Update()
    {
        if (Mathf.Abs(transform.position.x - redBot.position.x) < TriggerDistance) {
            var cam = Camera.main.GetComponent<CameraEndBehaviour>();
            if (cam != null)
                cam.End();
        }
    }
}
