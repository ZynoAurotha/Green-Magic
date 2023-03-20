using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    private float _screenHalfWidth;
    private float _screenHalfHeight;
    [SerializeField] private Transform _maskLeft;
    [SerializeField] private Transform _maskRight;
    [SerializeField] private Transform _maskUp;
    //[SerializeField] private Transform _maskDown;
    [SerializeField] private float _BoundaryThickness;

    //protected Rigidbody2D CameraRidgidbody2D;
    private GameObject _character1;
    private GameObject _character2;
    private float _midPointX;
    [SerializeField] private float _cameraHeight;

    void Awake()
    {
        //CameraRidgidbody2D = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        _character1 = GameObject.FindGameObjectWithTag("Character1");
        _character2 = GameObject.FindGameObjectWithTag("Character2");      
    }

    // Update is called once per frame
    void LateUpdate()
    {
        _screenHalfHeight = Camera.main.orthographicSize;
        _screenHalfWidth = Camera.main.aspect * _screenHalfHeight;
        OnBoundaryGenerator();
        OnCalculateCameraPosition();
    }

    private void OnCalculateCameraPosition()
    {
        _midPointX = (_character1.transform.position.x + _character2.transform.position.x) / 2;
        transform.position = new Vector3(_midPointX,_cameraHeight,-10);
    }

    private void OnBoundaryGenerator()
    { 
        _maskLeft.position = transform.position - (_screenHalfWidth * Vector3.right);
        _maskLeft.parent = transform;
		_maskLeft.localScale = new Vector3 (_BoundaryThickness, _screenHalfHeight * 2, 0);

        _maskRight.position = transform.position + (_screenHalfWidth * Vector3.right);
        _maskRight.parent = transform;
		_maskRight.localScale = new Vector3 (_BoundaryThickness, _screenHalfHeight * 2, 0);

        _maskUp.position = transform.position + (_screenHalfHeight * Vector3.up);
        _maskUp.parent = transform;
		_maskUp.localScale = new Vector3 (_screenHalfWidth * 2 + 2 * _BoundaryThickness, _BoundaryThickness, 0);

        // _maskDown.position = transform.position - (_screenHalfHeight * Vector3.up + (_BoundaryThickness/2) * Vector3.up);
        // _maskDown.parent = transform;
		// _maskDown.localScale = new Vector3 (_screenHalfWidth * 2 + 2 * _BoundaryThickness, _BoundaryThickness, 0);  
    }    
}
