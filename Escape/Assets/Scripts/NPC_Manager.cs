using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC_Manager : MonoBehaviour
{
    Animator animator;
    NavMeshAgent navMeshAgent;
    AudioSource audioSource;

    public float mood;

    public GameObject walkTarget;
    //bool following;  use later to distinguish between walking to and following
    public Transform turnTarget;
    public Transform lookTarget;

    public Transform neckBone;
    float maxNeckRotation = 45.0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();

        mood = 0.5f;
        SetMood(mood);
    }

    
    void Update()
    {
        Blink();
        /*
        ChangeMood(-1.0f + 2.0f * Mathf.PerlinNoise(Time.time * 0.01f, 0.0f));
        Debug.Log(-1.0f + 2.0f *  Mathf.PerlinNoise(Time.time * 0.01f, 0.0f));
        */

       
        if (Time.time > 1.0f)
        {
            WalkTo(walkTarget);
        }
    }

   


    private void LateUpdate()
    {
        if (turnTarget != null)
        {
            Vector3 relativePos = new Vector3(turnTarget.position.x - transform.position.x, 0.0f, turnTarget.position.z - transform.position.z); //Only rotate around y-Axis               
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = rotation;
        }

        if (lookTarget != null)
        {
            
            Vector3 relativePos = lookTarget.position - neckBone.transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);

            float angle = Vector3.Angle(relativePos,transform.forward);
            if (angle > -maxNeckRotation && angle < maxNeckRotation)
            {
                neckBone.transform.rotation = rotation;
                neckBone.transform.Rotate(0, 0, -90);
            }
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
            turnTarget = null;
            navMeshAgent.SetDestination(walkTarget.transform.position);
            animator.SetBool("walk", true);
            //audioSource.Play();
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                animator.SetBool("walk", false);
                walkTarget = null;
            }

        }
    }

    public void TurnTo(GameObject turnToTarget)
    {
        if (turnToTarget != null)
            turnTarget = turnToTarget.transform;
    }

    public void LookAt(GameObject lookAtTarget)
    {
        if (lookAtTarget != null)
            lookTarget = lookAtTarget.transform;
    }
}
