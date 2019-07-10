using UnityEngine;

public class CameraOrbit : MonoBehaviour
{

    protected Transform _XForm_Camera;
    protected Transform _XForm_Parent;

    protected Vector3 _LocalRotation;
    protected float _CamereDistance = 10f;

    public float MouseSensitivity = 4f;
    public float ScrollSensitivity = 2f;

    public float OrbitDampening = 10f;
    public float ScrollDampening = 6f;

    public bool CameraDisabled = false;



    // Start is called before the first frame update
    void Start()
    {
        this._XForm_Camera = this.transform;
        this._XForm_Parent = this.transform.parent;
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        CameraDisabled = !CameraDisabled;

        if(!CameraDisabled)
        {
            // Rotation of the Camera based on the Mouse Coordinates
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                _LocalRotation.x += Input.GetAxis("Mouse X") * MouseSensitivity;
                _LocalRotation.y -= Input.GetAxis("Mouse Y") * MouseSensitivity;

                // Clamp the y Rotation to horizon and not flipping over the top
                /*if (_LocalRotation.y < 0f)
                    _LocalRotation.y = 0f;
                else if (_LocalRotation.y > 90f)
                    _LocalRotation.y = 90f; */
                _LocalRotation.y = Mathf.Clamp(_LocalRotation.y, 0f, 90f);
            }

            // Zooming of the Camera based on the Scrolling
            if (Input.GetAxis("Mouse ScrollWeel") != 0f)
            {
                float ScrollAmount = Input.GetAxis("Mouse ScrollWeel") * ScrollSensitivity;
                
                // Makes camera zoom slower the closer it is to the target
                ScrollAmount *= (this._CamereDistance * 0.3f);

                this._CamereDistance += ScrollAmount * -1f;
                // This means the camera goes closer than 1.5 meters from the target, and no further than 500 meters
                this._CamereDistance = Mathf.Clamp(this._CamereDistance, 1.5f, 500f);
            }
        }
        
        // Actual Camera Rig Transformation
        Quaternion QT = Quaternion.Euler(_LocalRotation.y, _LocalRotation.x, 0);
        this._XForm_Parent.rotation = Quaternion.Lerp(this._XForm_Parent.rotation, QT, Time.deltaTime * OrbitDampening);

        if (this._XForm_Camera.localPosition.z != this._CamereDistance * -1f)
        {
            this._XForm_Camera.localPosition = new Vector3(0f, 0f, Mathf.Lerp(this._XForm_Camera.localPosition.z, this._CamereDistance * -1f, Time.deltaTime * ScrollDampening));
        }
    }
}
