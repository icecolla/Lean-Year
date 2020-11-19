using UnityEngine;
using UnityEngine.AI;

public class RunAway : State
{
    public RunAway(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) : base(_npc, _agent, _anim, _player)
    {
        name = STATE.RUNAWAY;
        agent.speed = 7;
        agent.isStopped = false;
    }

    public override void Enter()
    {
        agent.SetDestination(GameEnvironment.Singleton.SafeLocation.transform.position);
        anim.SetTrigger("isRunning");
        base.Enter();
    }
    public override void Update()
    {
        if (agent.remainingDistance < 1)
        {
            nextState = new Idle(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }
    }
    public override void Exit()
    {
        anim.ResetTrigger("isRunning");
        base.Exit();
    }

}
