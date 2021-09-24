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
    private List<GameObject> targets;
    private int last;
    public static AICharacterControl instance;

    private void Start()
    {
        // get the components on the object we need ( should not be null due to require component so no need to check )
        agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
        character = GetComponent<AICharacter>();

	    agent.updateRotation = false;
	    agent.updatePosition = true;
        targets = SceneIAController.instance.targets;
        last = 1;
        target = targets[0].transform;
        agent.SetDestination(target.position);
    }

    private void Update()
    {
        if (agent.remainingDistance > agent.stoppingDistance)
        {
            character.Move(agent.desiredVelocity, false);
        }
        else if (agent.remainingDistance == agent.stoppingDistance)
        {
            if (last < targets.Count - 1)
            {
                last++;
                target = targets[last].transform;
                agent.SetDestination(target.position);
            }
            else
            {
                SceneIAController.instance.destroyDistimia();
            }
        }
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
