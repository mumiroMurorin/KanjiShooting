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

    private Vector3 newAngle;

    private void Start()
    {
        newAngle = playerTransform.localEulerAngles;
    }

    /// <summary>
    /// âÒì]èàóù
    /// </summary>
    /// <param name="rotate">âÒì]Ç≥ÇπÇÈäpìx</param>
    public void Rotate(Vector3 rotation)
    {
        rotation *= magnitude;
        newAngle.y += rotation.y * (isReverseHorizontal ? -1 : 1);
        newAngle.x += rotation.x * (isReverseVertical ? -1 : 1);

        //Debug.Log($"angle{newAngle}");
        if (0 > newAngle.x && newAngle.x < verticalAngleMax) { newAngle.x = verticalAngleMax; }
        if (0 < newAngle.x && newAngle.x > verticalAngleMin) { newAngle.x = verticalAngleMin; }

        playerTransform.localEulerAngles = newAngle;
        mapCameraTransform.localEulerAngles = new Vector3(mapCameraTransform.eulerAngles.x, newAngle.y, 0);
    }
}
