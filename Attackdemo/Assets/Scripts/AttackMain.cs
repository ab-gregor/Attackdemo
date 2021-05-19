using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMain : MonoBehaviour
{

    public playerController2D playerControllerReferenceWhichHasATurnOnAndTurnOffBoolean;
    public Animator animatorOfThePlayer;
    public BoxCollider2D myAtackBoxCollider;

    private Statuses statusSciptOfEnemy;
    private Rigidbody2D enemyRigidBody;
    private bool isGrounded;
    private bool canAttack = true;
    private int attackIDCounterWhichIsUsedToControlWhichAttackIsToBeExecuted = 10; // In case the user doesn't input a number. This represents the number of attacks, starting from zero
    private float playerFacingDirection;

         
    
    public Transform groundCheck;
    public Transform groundCheckR;
    public Transform groundCheckL;

    public float pushBackForceOfFirstAttack;
    public int damageOfFirstAttack;
    public float windingUpTimeOfFirstAttack;
    public int damageOfSecondAttack;
    public float windingUpTimeOfSecondAttack;
    public int damageOfThirdAttack;
    public float windingUpTimeOfThirdAttack;
    
    // Start is called before the first frame update
    void Start()
    {
     
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfPlayerIsGrounded();
        CheckPlayerFacingDirection();
        CheckIfPlayerCanAttackAndExecuteAttackIfThePlayerCan();
    }

    private void CheckIfPlayerCanAttackAndExecuteAttackIfThePlayerCan()
    {
        if (canAttack)
        {
            StartCoroutine(AttackWhenverAttackButtonIsPressedAndEnemyRigidbodyWithAnAttachedStatusScriptIsAvailable());
        }
    }



    public void CheckIfPlayerIsGrounded()
    {
        if ((Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"))) ||
                 (Physics2D.Linecast(transform.position, groundCheckR.position, 1 << LayerMask.NameToLayer("Ground"))) ||
                  (Physics2D.Linecast(transform.position, groundCheckL.position, 1 << LayerMask.NameToLayer("Ground"))))
        {
            isGrounded = true;

        }
        else
        {
            isGrounded = false;
        }
    }





    public void IfEnemyHasBeenDetectedThenPushTheEnemyAndAlsoPlayTheAppropriateAnimation()
    {
        if (enemyRigidBody)
        {
            CalculateTheForceAsVectorAndAddItToTheEnemyRigicbody();

            PlayAppropriateAnimationDependentOnPlayerIsGroundedBoolean();

            enemyRigidBody = null;

        }

        else
        {
            PlayAppropriateAnimationDependentOnPlayerIsGroundedBoolean();
        }
    }

    private void CalculateTheForceAsVectorAndAddItToTheEnemyRigicbody()
    {
        Vector2 pushBackForceToAddAsVector = new Vector2(playerFacingDirection * pushBackForceOfFirstAttack, 0f);
        enemyRigidBody.AddForce(pushBackForceToAddAsVector, ForceMode2D.Impulse);
    }

    private void PlayAppropriateAnimationDependentOnPlayerIsGroundedBoolean()
    {
        if (!isGrounded)
        {
            animatorOfThePlayer.Play("attack_JumpKick");
        }
        else
        {
            animatorOfThePlayer.Play("attack_Kick");
        }
    }


    private void OnTriggerStay2D(Collider2D collision) // This function is automatically called by unity like the update function
    {
        if (collision.tag == "Enemy")
        {
            Debug.Log("Collision with enemy successfully detected");

            enemyRigidBody = collision.gameObject.GetComponent<Rigidbody2D>();

        }
    }


    public void CheckPlayerFacingDirection()
    {
        if (gameObject.transform.rotation.y == 0)
        {
            playerFacingDirection = 1; // 1 means right

        }
        else if (gameObject.transform.rotation.y == -1)
        {
            playerFacingDirection = -1;

        }

    }





    public IEnumerator AttackWhenverAttackButtonIsPressedAndEnemyRigidbodyWithAnAttachedStatusScriptIsAvailable()
    {
        if (enemyRigidBody)
        {
            statusSciptOfEnemy = enemyRigidBody.gameObject.GetComponent<Statuses>();

        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
                
            attackIDCounterWhichIsUsedToControlWhichAttackIsToBeExecuted += 1;


            if (attackIDCounterWhichIsUsedToControlWhichAttackIsToBeExecuted > 2)
            {
                attackIDCounterWhichIsUsedToControlWhichAttackIsToBeExecuted = 0;
            }
                                             

            if (attackIDCounterWhichIsUsedToControlWhichAttackIsToBeExecuted == 0)
            {
                playerControllerReferenceWhichHasATurnOnAndTurnOffBoolean.canMove = false;
                canAttack = false;

                yield return new WaitForSeconds(windingUpTimeOfFirstAttack);

                IfEnemyHasBeenDetectedThenPushTheEnemyAndAlsoPlayTheAppropriateAnimation();

                if (statusSciptOfEnemy)
                {
                    statusSciptOfEnemy.DecreaseHealthByTheNumber(damageOfFirstAttack);
                }

                statusSciptOfEnemy = null;
                playerControllerReferenceWhichHasATurnOnAndTurnOffBoolean.canMove = true;
                canAttack = true;
            }

            if(attackIDCounterWhichIsUsedToControlWhichAttackIsToBeExecuted == 1)
            {
                canAttack = false;
                playerControllerReferenceWhichHasATurnOnAndTurnOffBoolean.canMove = false;

                yield return new WaitForSeconds(windingUpTimeOfSecondAttack);

                if (statusSciptOfEnemy)
                {
                    statusSciptOfEnemy.DecreaseHealthByTheNumber(damageOfSecondAttack);
                }

                animatorOfThePlayer.Play("attack_leftPunch");
                
                statusSciptOfEnemy = null;
                playerControllerReferenceWhichHasATurnOnAndTurnOffBoolean.canMove = true;
                canAttack = true;
            }

            if(attackIDCounterWhichIsUsedToControlWhichAttackIsToBeExecuted == 2)
            {
                canAttack = false;
                playerControllerReferenceWhichHasATurnOnAndTurnOffBoolean.canMove = false;

                yield return new WaitForSeconds(windingUpTimeOfThirdAttack);

                if(statusSciptOfEnemy)
                {
                    statusSciptOfEnemy.DecreaseHealthByTheNumber(damageOfThirdAttack);
                }

                animatorOfThePlayer.Play("attack_rightPunch");

                statusSciptOfEnemy = null;
                playerControllerReferenceWhichHasATurnOnAndTurnOffBoolean.canMove = true;
                canAttack = true;
            }


        }
        
    }
}
