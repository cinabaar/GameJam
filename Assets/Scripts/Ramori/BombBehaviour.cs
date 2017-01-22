using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehaviour : MonoBehaviour
{
    public void Boom()
    {
        Screenshake.Shake(2f, 1f);
    }
}
