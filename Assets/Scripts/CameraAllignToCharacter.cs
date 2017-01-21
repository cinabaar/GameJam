using UnityEngine;

[ExecuteInEditMode]
public class CameraAllignToCharacter : MonoBehaviour
{
    [SerializeField] Vector2 _characterOffset;

    private Transform _redBotTransform;
    void Awake()
    {
        _redBotTransform = GameObject.Find("RedBot").transform;        
    }

    void Update()
    {
        transform.position = _redBotTransform.position + new Vector3(_characterOffset.x, _characterOffset.y, -10);
    }
    
}
