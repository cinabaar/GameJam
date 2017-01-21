using UnityEngine;

[ExecuteInEditMode]
public class CameraAllignToCharacter : MonoBehaviour
{
    [SerializeField] Vector2 _characterOffset;

    private Vector3 _desiredLocation;
    private Vector3 _currentVelocity;

    private Transform _redBotTransform;
    void Awake()
    {
        _redBotTransform = GameObject.Find("RedBot").transform;        
    }

    void Update()
    {
        _desiredLocation =  _redBotTransform.position + new Vector3(_characterOffset.x, _characterOffset.y, -10);
        transform.position = Vector3.SmoothDamp(transform.position, _desiredLocation, ref _currentVelocity, 0.2f, 100, Time.deltaTime);
    }
    
}
