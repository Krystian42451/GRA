using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    

    Animator animator;
    int isWalkingHash;
    int isRunningHash;
    int isJumpingHash;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isJumpingHash = Animator.StringToHash("isJumping");
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool checkWalk = Input.GetKey(KeyCode.W);
        bool isRunning = animator.GetBool(isRunningHash);
        bool checkRun = Input.GetKey(KeyCode.LeftShift);
        bool isJumping = animator.GetBool(isJumpingHash);
        bool checkJump = Input.GetKey(KeyCode.Space);

        //chodzenie
        if (!isWalking && checkWalk)
        {
            animator.SetBool(isWalkingHash, true);
        }
        if (isWalking && !checkWalk) 
        {
            animator.SetBool(isWalkingHash, false);
        }
        //Bieganie
        if (!isRunning && (checkWalk && checkRun))
        {
            animator.SetBool(isRunningHash, true);

        }
        if (isRunning && (!checkWalk || !checkRun))
        {
            animator.SetBool(isRunningHash, false);
            
            
            
        }
        //Skakanie
        if (!isJumping && checkJump)
        {
            animator.SetBool(isJumpingHash, true);
        }
        if (isJumping && !checkJump)
        {
            animator.SetBool(isJumpingHash, false);
        }

    }
}
