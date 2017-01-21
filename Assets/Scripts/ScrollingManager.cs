using System.Collections.Generic;
using UnityEngine;

public class ScrollingManager : MonoBehaviour
{

    private static ScrollingManager _instance;

    public List<ScrollingBehaviour> Scrolls = new List<ScrollingBehaviour>();
    public float Speed = 0;

    public void Awake()
    {
        _instance = this;
    }

    public static void Register(ScrollingBehaviour scroll)
    {
        if (_instance == null)
            return;

        _instance.Scrolls.Add(scroll);
    }

    public static void Unregister(ScrollingBehaviour scroll)
    {
        if (_instance == null)
            return;

        _instance.Scrolls.Remove(scroll);
    }

    public static void SetSpeed(float speed)
    {
        if (_instance == null)
            return;
        _instance.Speed = speed;
    }

    public void Update()
    {
        foreach (var s in _instance.Scrolls)
        {
            s.Scroll(Speed * Time.deltaTime);
        }
    }
}