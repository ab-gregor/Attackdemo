using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMain : MonoBehaviour
{
    public bool canAttack = true;
    public playerController2D playerControllerReference;


    public int attackIDCounterWhichIsUsedToControlWhichAttackIsToBeExecuted;
    public Attack pushBackScript;
    public int damageOfFirstAttack;
    public float windingUpTimeOfFirstAttack;
    public int damageOfSecondAttack;
    public float windingUpTimeOfSecondAttack;
    public int damageOfThirdAttack;
    public float windingUpTimeOfThirdAttack;
    public Statuses statusSciptOfEnemy;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        attackIDCounterWhichIsUsedToControlWhichAttackIsToBeExecuted = 10;// any number greater than the topmost number
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (canAttack)
        {
            StartCoroutine(AttackWhenAttackButtonIsPressed());
        }
    }

    public IEnumerator AttackWhenAttackButtonIsPressed()
    {
        if(pushBackScript.enemyRigidBody)
        {
            statusSciptOfEnemy = pushBackScript.enemyRigidBody.gameObject.GetComponent<Statuses>();
            //Debug.Log("Entered the main attack script");


            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Debug.Log("Main attack script is working");
                attackIDCounterWhichIsUsedToControlWhichAttackIsToBeExecuted += 1;

                if(attackIDCounterWhichIsUsedToControlWhichAttackIsToBeExecuted > 2)
                {
                    attackIDCounterWhichIsUsedToControlWhichAttackIsToBeExecuted = 0;
                }

                if(attackIDCounterWhichIsUsedToControlWhichAttackIsToBeExecuted == 0)
                {
                    playerControllerReference.canMove = false;
                    canAttack = false;
                    yield return new WaitForSeconds(windingUpTimeOfFirstAttack);
                    pushBackScript.isAttackButtonPressed = true;
                    Debug.Log("Attack button pressed part");
                    statusSciptOfEnemy.DecreaseHealthByTheNumber(damageOfFirstAttack);

                    
                    
                    statusSciptOfEnemy = null;
                    playerControllerReference.canMove = true;
                    canAttack = true;
                }

                if(attackIDCounterWhichIsUsedToControlWhichAttackIsToBeExecuted == 1)
                {
                    canAttack = false;
                    playerControllerReference.canMove = false;
                    yield return new WaitForSeconds(windingUpTimeOfSecondAttack);
                    statusSciptOfEnemy.DecreaseHealthByTheNumber(damageOfSecondAttack);
                    animator.Play("attack_leftPunch");
                    Debug.Log("Doing second attack");
                    statusSciptOfEnemy = null;
                    playerControllerReference.canMove = true;
                    canAttack = true;
                }

                if(attackIDCounterWhichIsUsedToControlWhichAttackIsToBeExecuted == 2)
                {
                    canAttack = false;
                    playerControllerReference.canMove = false;
                    yield return new WaitForSeconds(windingUpTimeOfThirdAttack);
                    statusSciptOfEnemy.DecreaseHealthByTheNumber(damageOfThirdAttack);
                    animator.Play("attack_rightPunch");
                    Debug.Log("Doing third attack");
                    statusSciptOfEnemy = null;
                    playerControllerReference.canMove = true;
                    canAttack = true;
                }


            }
        }
    }
}
