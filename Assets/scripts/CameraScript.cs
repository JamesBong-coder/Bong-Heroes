using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Camera-Control/CameraScript")]
public class CameraScript : MonoBehaviour
{
    //список для насройки осей вращения
    public enum RotationAxes {MouseXandY = 0, MouseX = 1, MouseY = 2};
    public RotationAxes axes = RotationAxes.MouseXandY;
    //чувствительность мыши
    public float SensitivityX = 2;
    public float SensitivityY = 2;
    //условия угла вращения осей х и у
    public float minX = -360f;
    public float maxX = 360f;
    public float minY = -360f;
    public float maxY = 360f;

    //текущий угол вращения
    public float rotationX = 0;
    public float rotationY = 0;

    Quaternion OrRotation;

    void Start()
    {
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }
        OrRotation = transform.localRotation;

    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) angle += 360;
        if (angle > 360) angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }

    void Update()
    {
        if (axes == RotationAxes.MouseXandY)
        {
            rotationX += Input.GetAxis("Mouse X") * SensitivityX;
            rotationY += Input.GetAxis("Mouse Y") * SensitivityY;

            rotationX = ClampAngle(rotationX, minX, maxX);
            rotationY = ClampAngle(rotationY, minY, maxY);

            Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);
            transform.localRotation = OrRotation * xQuaternion * yQuaternion;
        }
        else if(axes == RotationAxes.MouseX)
        {
            rotationX += Input.GetAxis("Mouse X") * SensitivityX;

            rotationX = ClampAngle(rotationX, minX, maxX);

            Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            transform.localRotation = OrRotation * xQuaternion;
        }
        else if (axes == RotationAxes.MouseY)
        {
            rotationY += Input.GetAxis("Mouse Y") * SensitivityY;

            rotationY = ClampAngle(rotationY, minY, maxY);

            Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);
            transform.localRotation = OrRotation * yQuaternion;
        }
    }
    public void SetRotation(float rot)
    {
        SensitivityX = rot;
        SensitivityY = rot;
        gameObject.GetComponentInChildren<Camera>().GetComponent<CameraScript>().SensitivityX = rot;
        gameObject.GetComponentInChildren<Camera>().GetComponent<CameraScript>().SensitivityY = rot;
    }
}
