using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpscareObject : MonoBehaviour
{
    [SerializeField] private string soundName = "Jumpscare1";
    [SerializeField] float speed = 17f;


    private bool hasScared = false;
    private bool isMoving = false;
    public enum AxisToRotate { Back, Down, Forward, Left, Right, Up, Zero };
    static readonly Vector3[] vectorAxes = new Vector3[] {
        Vector3.back,
        Vector3.down,
        Vector3.forward,
        Vector3.left,
        Vector3.right,
        Vector3.up,
        Vector3.zero
    };

    public AxisToRotate myAxis;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (isMoving)
        {
            transform.Translate(GetAxis(myAxis) * speed * Time.deltaTime);
        }
    }
    public Vector3 GetAxis(AxisToRotate axis)
    {
        return vectorAxes[(int)axis];
    }
    public void StaticObjectScarePlayer()
    {
        if (!hasScared)
        {
            AudioManager.instance.Play(soundName);
            isMoving = true;
            Destroy(this.gameObject, 1.5f);
            hasScared = true;
        }
    }
}
