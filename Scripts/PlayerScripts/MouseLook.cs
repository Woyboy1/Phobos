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

    // Game Handling:
    private bool canLook = true;

    public float MouseSensitivity
    {
        get { return mouseSensitivity; }
        set { mouseSensitivity = value; }
    }

    public bool CanLook
    {
        get { return canLook; }
        set { canLook = value; }
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (canLook)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f).normalized;
            playerBody.Rotate(Vector3.up * mouseX);

            HeadBobbing();
        }
    }

    private void HeadBobbing() // super annoying and weird way to implement headbobbing but it's better than calculating a lerp
    {
        if (Input.GetKey(KeyCode.W) || (Input.GetKey(KeyCode.A) || (Input.GetKey(KeyCode.S) || (Input.GetKey(KeyCode.D)))))
            StartBobbing();

        // Stop bobbing

        if (Input.GetKeyUp(KeyCode.W) || (Input.GetKeyUp(KeyCode.A) || (Input.GetKeyUp(KeyCode.S) || (Input.GetKeyUp(KeyCode.D)))))
            StopBobbing();
    }

    private void StartBobbing()
    {
        gameObject.GetComponent<Animator>().Play("HeadBobbingPlayer");
    }

    private void StopBobbing()
    {
        gameObject.GetComponent<Animator>().Play("New State");
    }
}
