using System;
using UnityEngine;
using System.Collections.Generic;


[RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof (AICharacter))]
public class AICharacterControl : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
    public AICharacter character { get; private set; }                         // the character we are controlling
    private Transform target;                                                   // target to aim for
    public List<GameObject> targets;
    private int last;
    public static AICharacterControl instance;

    private void Start()
    {
        // get the components on the object we need ( should not be null due to require component so no need to check )
        agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
        character = GetComponent<AICharacter>();

	    agent.updateRotation = false;
	    agent.updatePosition = true;
        targets = SceneController.instance.targets;
        last = 0;
    }


    private void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
        } else {
            target = getLast();
            agent.SetDestination(target.position);
        }
        if (agent.remainingDistance > agent.stoppingDistance)
        {
            character.Move(agent.desiredVelocity, false);
        }
        if (agent.remainingDistance == agent.stoppingDistance)
        {
            target = getNext();
        }
        else
        {
            character.Move(Vector3.zero, false);
        }
    }
    public Transform getNext()
    {
        if (last < targets.Count)
        {
            last++;
            return targets[last].transform;
        }
        else
        {
            SceneController.instance.destroyDistimia();
            return null;
        }
    }

    public Transform getLast()
    {
        return targets[last].transform;
    }
    public int getLastPersistencia()
    {
        return last;
    }
    public void cargarLastTarget(int last)
    {
        this.last = last;
    }

}
