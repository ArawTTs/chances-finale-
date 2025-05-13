using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementScript : MonoBehaviour
{
    private int[] walkingSequence = { 0, 1, 0, 2, 0 }; 
    private int currentSequenceIndex = 0; 

    public float walkingTimer = 0;
    public float walkingLimit;
    public int walkingStyle = 0;

    public float walkingSpeed;

    private bool isWalkingUp=false;
    private bool isWalkingDown=false;
    private bool isWalkingLeft=false;
    private bool isWalkingRight=false;

    public Animator walkingAnim;

    void Start()
    {
        isWalkingUp = false;
        isWalkingDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        walkingTimer += Time.deltaTime;
        if (walkingTimer > walkingLimit)
        {
            walkingTimer = 0;
            currentSequenceIndex = (currentSequenceIndex + 1) % walkingSequence.Length; // Cycle through the sequence
            walkingStyle = walkingSequence[currentSequenceIndex];
        }

        switch (walkingStyle)
        {
            case 0:
                Debug.Log("Walking Style 0: Casual");
                isWalkingUp = false;
                isWalkingDown = false;
                walkingAnim.SetBool("WalkUp", false);
                walkingAnim.SetBool("WalkDown", false);
                break;

            case 1:
                Debug.Log("Walking Style 1: Fast");
                isWalkingUp = true;
                isWalkingDown = false;
                upWalking();
                break;

            case 2:
                Debug.Log("Walking Style 2: Stealthy");
                isWalkingUp = false;
                isWalkingDown = true;
                downWalking();
                break;

            default:
                Debug.LogWarning("Unknown walking style");
                break;
        }

        //Debug.Log(walkingTimer);
    }

    public void upWalking()
    {
        if(isWalkingUp)
        {
            walkingAnim.SetBool("WalkUp", true);
            transform.position += Vector3.forward * walkingSpeed * Time.deltaTime;
        }
    }
    public void downWalking()
    {
        if (isWalkingDown)
        {
            walkingAnim.SetBool("WalkDown", true);
            transform.position += Vector3.back * walkingSpeed * Time.deltaTime;
        }
    }

    public void leftWalking()//not used yet
    {
        if (isWalkingLeft)
        {
            walkingAnim.SetBool("WalkLeft", true);
            transform.position += Vector3.left * walkingSpeed * Time.deltaTime;
        }
    }
    public void rightWalking()//not used yet
    {
        if (isWalkingRight)
        {
            walkingAnim.SetBool("WalkRight", true);
            transform.position += Vector3.right * walkingSpeed * Time.deltaTime;
        }
    }
}
