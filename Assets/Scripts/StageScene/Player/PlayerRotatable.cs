using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotatable : MonoBehaviour, IRotatable
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform mapCameraTransform;
    [SerializeField] private float magnitude = 1f;
    [SerializeField] private float verticalAngleMin;
    [SerializeField] private float verticalAngleMax;
    [SerializeField] private bool isReverseVertical;
    [SerializeField] private bool isReverseHorizontal;

    private Vector3 addAngle;

    private void Update()
    {
        Vector3 newAngle = playerTransform.localEulerAngles;
        newAngle += addAngle * Time.deltaTime;
        newAngle = NormalizeAngle(newAngle);

        addAngle = Vector3.zero;

        if (0 > newAngle.x && newAngle.x < verticalAngleMax) { newAngle.x = verticalAngleMax; }
        if (0 < newAngle.x && newAngle.x > verticalAngleMin) { newAngle.x = verticalAngleMin; }

        //Debug.Log($"angle{newAngle}");

        playerTransform.localEulerAngles = newAngle;
        mapCameraTransform.localEulerAngles = new Vector3(mapCameraTransform.eulerAngles.x, newAngle.y, 0);
    }

    /// <summary>
    /// âÒì]èàóù
    /// </summary>
    /// <param name="rotate">âÒì]Ç≥ÇπÇÈäpìx</param>
    public void Rotate(Vector2 rotation)
    {
        addAngle = new Vector3();

        rotation *= magnitude;
        addAngle.y = rotation.y * (isReverseHorizontal ? -1 : 1);
        addAngle.x = rotation.x * (isReverseVertical ? -1 : 1);

        //Debug.Log($"angle{newAngle}");
    }

    private static Vector3 NormalizeAngle(Vector3 angle)
    {
        angle.x = Mathf.Repeat(angle.x + 180f, 360f) - 180f;
        angle.y = Mathf.Repeat(angle.y + 180f, 360f) - 180f;
        angle.z = Mathf.Repeat(angle.z + 180f, 360f) - 180f;
        return angle;
    }
}
