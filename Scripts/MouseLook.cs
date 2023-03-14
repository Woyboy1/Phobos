using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    /// <summary>
    /// Basic mouse look function using the unity input manager.
    /// This also rotates the player as well.
    /// </summary>

    [Header("Sensitivity")]
    [SerializeField] private float mouseSensitivity = 100f;

    [Header("References")]
    [SerializeField] private Transform playerBody;

    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f).normalized;
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
