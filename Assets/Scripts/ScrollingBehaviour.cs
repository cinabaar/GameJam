using UnityEngine;

public class ScrollingBehaviour : MonoBehaviour {

    [SerializeField]
    float ScrollScale = 1f;

    public void Start()
    {
        ScrollingManager.Register(this);
    }

    public void OnDestroy()
    {
        ScrollingManager.Unregister(this);
    }

    public void Scroll(float distance)
    {
        transform.localPosition += new Vector3(ScrollScale * distance, 0f, 0f);
    }
}
