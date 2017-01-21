using UnityEngine;

public class ExplosionBehaviour : MonoBehaviour
{
    public void OnAnimEnd()
    {
        Destroy(gameObject);
    }
}
