using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC_Animation : MonoBehaviour
{
    Animator animator;
    NavMeshAgent navMeshAgent;
    

    public float mood;

    private GameObject walkTarget;
    
    private GameObject turnTarget;
   

    //For LookAt Feature:
    public Transform neckBone;
    float maxNeckRotation = 45.0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        

        mood = 0.5f;
        SetMood(mood);
    }

    
    void Update()
    {
        Blink();
       
    }

   


    private void LateUpdate()
    {
       

        if (turnTarget != null)
        {          
            Vector3 relativePos = new Vector3(turnTarget.transform.position.x - transform.position.x, 0.0f, turnTarget.transform.position.z - transform.position.z); //Only rotate around y-Axis
            Quaternion newRotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.time *0.01f);          
        }
        
    }

    void Blink()
    {
        if (Random.Range(0.0f, 1.0f) > 0.99f) { animator.SetTrigger("blink"); }
    }

    public void SetMood(float mood)
    {
        animator.SetFloat("mood", mood);
    }

    public void ChangeMood(float delta)
    {
        mood += delta;
        if (mood < 0.0f) mood = 0.0f;
        if (mood > 1.0f) mood = 1.0f;
        animator.SetFloat("mood", mood);
    }

    public void WalkTo(GameObject walkTarget)
    {
        if (walkTarget != null)
        {
            
            navMeshAgent.SetDestination(walkTarget.transform.position);
            animator.SetBool("walk", true);
            
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                animator.SetBool("walk", false);
                walkTarget = null;
            }

        }

       
       
    }

    public void TurnTo(GameObject other)
    {
        turnTarget = other;
    }

}
