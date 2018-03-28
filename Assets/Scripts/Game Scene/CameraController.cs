using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Limits
{
    public float minValue;
    public float maxValue;

    public Limits(float min, float max)
    {
        minValue = min;
        maxValue = max;
    }

    public static Limits defaultCameraVrtcalLimits = new Limits(5, 9.2f);
    public static Limits defaultCameraHztalLimits = new Limits(5, 0);

    public override string ToString()
    {
        return string.Format("Min: {0} - Max: {1}", minValue, maxValue);
    }
}

public class CameraController : MonoBehaviour {

    [SerializeField]
    [Range(1, 20)]
    private float speed = 10;

    private Limits horizontalLimits;
    private Limits verticalLimits;

    private void Update()
    {
        transform.Translate(transform.right * Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime + transform.up * Input.GetAxisRaw("Vertical") * speed * Time.deltaTime);

        Vector3 clampedPos = transform.position;
        clampedPos.x = Mathf.Clamp(clampedPos.x, horizontalLimits.minValue, horizontalLimits.maxValue);
        clampedPos.y = Mathf.Clamp(clampedPos.y, verticalLimits.minValue, verticalLimits.maxValue);

        transform.position = clampedPos;
    }

    public void SetCameraLimits(Limits hLimits, Limits vLimits)
    {
        horizontalLimits = hLimits;
        verticalLimits = vLimits;
    }
}
