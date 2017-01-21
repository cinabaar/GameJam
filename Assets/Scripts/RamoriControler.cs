using UnityEngine;

public class RamoriControler : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) {
            Screenshake.Shake(1f, 0.5f);
        }
        if (Input.GetKeyDown(KeyCode.O)) {
            Screenshake.Shake(0f, 1f, 0.5f);
        }
        if (Input.GetKeyDown(KeyCode.P)) {
            Screenshake.Shake(1f, 0f, 0.5f);
        }

        if (Input.GetKey(KeyCode.Z)) {
            ScrollingManager.SetSpeed(10f);
        }
        if (Input.GetKey(KeyCode.C)) {
            ScrollingManager.SetSpeed(-10f);
        }
    }
}