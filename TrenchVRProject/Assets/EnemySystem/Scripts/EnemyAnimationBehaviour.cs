using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationBehaviour : StateMachineBehaviour
{
    [System.Flags]
    public enum Actions : int
    {
        None = 0,
        Move = 1,
        Facing = 2,
        Gain_Fear = 4,
        Lose_Fear = 8,
        Aim = 16,
        Shoot = 32,
        Reload = 64,
        Plan = 128,
        Reach_Waypoint = 256,
        Knee = 512,
        FixPos = 1024,
        Kill = 2048
    }

    [EnumFlags]
    public Actions onEnter;

    [EnumFlags]
    public Actions onUpdate;

    [EnumFlags]
    public Actions onExit;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Execute(onEnter, animator.GetComponent<EnemySystem.Enemy>());
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Execute(onUpdate, animator.GetComponent<EnemySystem.Enemy>());
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Execute(onExit, animator.GetComponent<EnemySystem.Enemy>());
    }

    private void Execute(Actions actions, EnemySystem.Enemy enemy)
    {
        if (actions.HasFlag(Actions.Aim))
        {
            enemy.Aim();
        }

        if (actions.HasFlag(Actions.Facing))
        {
            enemy.FaceObjective();
        }

        if (actions.HasFlag(Actions.Gain_Fear))
        {
            enemy.fear += Time.deltaTime;

            enemy.fear = Mathf.Clamp(enemy.fear, 0, 100);
        }

        if (actions.HasFlag(Actions.Lose_Fear))
        {
            enemy.fear -= Time.deltaTime;
            enemy.fear = Mathf.Clamp(enemy.fear, 0, 100);
        }

        if (actions.HasFlag(Actions.Move))
        {
            enemy.Moving();
        }

        if (actions.HasFlag(Actions.Shoot))
        {
            enemy.Attack();
        }

        if (actions.HasFlag(Actions.Plan))
        {
            enemy.Plan();
        }

        if (actions.HasFlag(Actions.Reload))
        {
            enemy.Reload();
        }

        if (actions.HasFlag(Actions.Reach_Waypoint))
        {
            enemy.ReachWaypoint();
        }

        if (actions.HasFlag(Actions.Knee))
        {
            enemy.Knee();
        }

        if (actions.HasFlag(Actions.FixPos))
        {
            enemy.FixPos();
        }

        if (actions.HasFlag(Actions.Kill))
        {
            enemy.Kill();
        }
    }
}
