using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpscareManager : MonoBehaviour
{
    /// <summary>
    /// This is NOT a singleton pattern. Will handle the jumpscare animation and
    /// post proccessing effects, etc.
    /// </summary>
    /// 

    [Header("Jumpscare objects")]
    [SerializeField] private GameObject jumpscareObject;

    [Header("Offsets for Ghoul Jumpscare")]
    [SerializeField] private float zGhoulPlayerOffset = 1.4f;
    [SerializeField] private float zGhoulCameraOffset = 60;
    [SerializeField] private float yGhoulCameraOffset = 30.0f;

    [Header("Offsets for other Jumpscare")]
    [SerializeField] private float empty;

    [Header("Audio")]
    [SerializeField] private float timeTilAudio = 0.2f;
    [SerializeField] private string ghoulJumpscareAudio = "PlayerJumpscare";

    [Header("Etc")]
    [SerializeField] private float timeTilRestart = 2.0f;

    Camera cam;
    PlayerMovement playerMovement;
    MouseLook playerLook;
    Player player;

    private void Start()
    {
        cam = Camera.main;
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        player = FindAnyObjectByType<Player>();
        playerLook = FindAnyObjectByType<MouseLook>();
    }

    public void GhoulJumpscare() // for Ghoul
    {
        PlayerControlsDisabled();

        // Instantiating a prefab of the jumpscare
        Vector3 playerOffset = new Vector3(player.transform.position.x, 0, player.transform.position.z - zGhoulPlayerOffset);
        GameObject jumpscareClone = Instantiate(jumpscareObject, playerOffset, Quaternion.identity);

        StartCoroutine(PlayGhoulAudio());

        // Moving the camera to face the jumpscare
        Vector3 cameraOffset = new Vector3(jumpscareClone.transform.position.x, zGhoulCameraOffset, jumpscareClone.transform.position.z - yGhoulCameraOffset);
        cam.gameObject.transform.LookAt(cameraOffset);

        StartCoroutine(EndGame());
    }

    public void PlayerControlsDisabled()
    {
        playerMovement.CanMove = false;
        playerLook.CanLook = false;
        player.StopBreathingSFX();
    }

    IEnumerator PlayGhoulAudio()
    {
        yield return new WaitForSeconds(timeTilAudio);
        AudioManager.instance.Play(ghoulJumpscareAudio);
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(timeTilRestart);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
