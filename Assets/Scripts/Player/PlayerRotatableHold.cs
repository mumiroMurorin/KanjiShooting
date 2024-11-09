using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotatableHold : MonoBehaviour, IRotatable
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform mapCameraTransform;
    [SerializeField] private float magnitude = 1f;
    [SerializeField] private float verticalAngleMin;
    [SerializeField] private float verticalAngleMax;
    [SerializeField] private bool isReverseVertical;
    [SerializeField] private bool isReverseHorizontal;

    private Vector3 newAngle;
    private Vector3 addAngle;

    private void Update()
    {
        //Debug.Log($"angle{newAngle}");
        newAngle = NormalizeAngle(playerTransform.localEulerAngles);

        if (addAngle.magnitude <= 0) { return; }

        newAngle += addAngle;

        if (0 > newAngle.x && newAngle.x < verticalAngleMax) { newAngle.x = verticalAngleMax; }
        if (0 < newAngle.x && newAngle.x > verticalAngleMin) { newAngle.x = verticalAngleMin; }

        playerTransform.localEulerAngles = newAngle;
        mapCameraTransform.localEulerAngles = new Vector3(mapCameraTransform.eulerAngles.x, newAngle.y, 0);
    }

    /// <summary>
    /// âÒì]èàóù
    /// </summary>
    /// <param name="rotate">âÒì]Ç≥ÇπÇÈäpìx</param>
    public void Rotate(Vector2 rotation)
    {
        // rotationÇÕäeóvëf1 Å` -1Ç≈Ç‚Ç¡ÇƒÇ≠ÇÈ

        rotation *= magnitude;
        addAngle.y = rotation.y * (isReverseHorizontal ? -1 : 1);
        addAngle.x = rotation.x * (isReverseVertical ? -1 : 1);
    }

    public void StopRotation()
    {
        addAngle = Vector3.zero;
    }

    private static Vector3 NormalizeAngle(Vector3 angle)
    {
        angle.x = Mathf.Repeat(angle.x + 180f, 360f) - 180f;
        angle.y = Mathf.Repeat(angle.y + 180f, 360f) - 180f;
        angle.z = Mathf.Repeat(angle.z + 180f, 360f) - 180f;
        return angle;
    }
}
