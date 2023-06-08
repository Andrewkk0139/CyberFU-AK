using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControls : MonoBehaviour
{
    //the speed of the enemy
    public float speed = 2;

    //the attacking distance for the enemy
    public float attackingDistance = 1;

    //axis points(x,y,z) for the direction for the enemy
    public Vector3 direction;

    //the animator component controls the animations for the enemy
    private Animator animatorEnemy;

    // the rigidbody to control the enemy
    private Rigidbody rigidbodyEnemy;

    //varible will help the enemy target any object with the varibles
    private Transform target;

    //if the enemy is following the target or not
    public bool isFollowingTarget;

    // if enemy is attacking the target or not
    public bool isAttackingTarget;
    // Start is called before the first frame update

    //set value of what the current time is for attacking
    private float chasingPlayer = 0.1f;

    //current value of what the current time is for attacking
    public float currentAttackingTime;

    //the set value of the maximum time of attacking
    public float maxAttackingTime = 2f;
    void Start()
    {
        isFollowingTarget = true;
        currentAttackingTime = maxAttackingTime;

    //getting the component for the animator
    animatorEnemy = GetComponent<Animator>();
    //getting the rigidbody
    rigidbodyEnemy = GetComponent<Rigidbody>();
    //set the target to follow the enemy
    target = GameObject.FindGameObjectWithTag("Player").transform;

    }


    void FollowTarget()
    {
        if(!isFollowingTarget){
            //isFollwoingTarget is true?
            return;
        }
        //if enemy is too far away to attack target
        if(Vector3.Distance(transform.position,target.position)>= attackingDistance)
        {
            //direction is equals to the targets position minus enemys position
            direction = target.position - transform.position;
            direction.y = 0;

            //Quaternions help game objects rotations
            //Enemy rotation to focus on the target
            transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(direction), 100);
            if(rigidbodyEnemy.velocity.sqrMagnitude != 0){
                //move the enemy by the value of speed
                rigidbodyEnemy.velocity = transform.forward * speed;
                //animation "Walk" is set to true
                animatorEnemy.SetBool("Walk", true);
            }
            //If enemys distance is close to attack the player
        } else if(Vector3.Distance(transform.position, target.position) <= attackingDistance){
            //Keeps the enemy from moving within its place
            rigidbodyEnemy.velocity = Vector3.zero;
            //animation "Walk" is set to false
            animatorEnemy.SetBool("Walk",false);
            //if the enemy is not following the target the 
            //boolean will be set to false since is near 
            //the target to attacl
            isFollowingTarget = false;
            //the enemy is now attacking the target so set
            //"isAttackingTarget" to true
            isAttackingTarget = true;
        }
    }

    void Attack(){
        if(!isAttackingTarget){
            return;
        }
        //the current attacking time add to the value of delta time
        currentAttackingTime += Time.deltaTime;
        //if the current attack time is greater than the max attack time
        if(currentAttackingTime > maxAttackingTime){
            //set the current attacking time to 0
            currentAttackingTime = 0f;
            //trigger the enemys attack animation
            animatorEnemy.SetTrigger("Attack1");
        }
        //if the enemys distance is greater than the attacking
        //distance and the value chasinf player
        if(Vector3.Distance(transform.position, target.position) > attackingDistance + chasingPlayer)
        {
            isAttackingTarget = false;
            isFollowingTarget = true;
        }
    }

    void FixedUpdate(){
        FollowTarget();
    }
    void Update(){
        Attack();
    }
}
