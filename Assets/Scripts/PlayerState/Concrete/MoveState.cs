using UnityEngine;

public class MoveState : State
{
    private readonly GameObject _indicator;
    public MoveState(StateManager stateManager, GameObject moveIndicatorPrefab) : base(stateManager)
    {
        _indicator = Object.Instantiate(moveIndicatorPrefab);
        _indicator.SetActive(false);
    }

    public override void ExitState()
    {
        _indicator.SetActive(false);
    }

    public override void CheckForChange()
    {
        var agent = stateManager.PlayerController.Agent;
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    stateManager.SwitchState(stateManager.IdleState);
                }
            }
        }
    }

    public override void EnterState()
    {
        stateManager.PlayerController.AnimationManager.Run();
        MoveAgent();
    }

    

    public override void UpdateState()
    {
        return;
    }

    private void MoveAgent()
    {
        var mousePosition = Input.mousePosition;
        var playerController = stateManager.PlayerController;

        Ray ray = playerController.Cemara.ScreenPointToRay(mousePosition);

        bool didHit = RaycastHitUtil.ExludePlayerLayer(ray, out RaycastHit hit);

        if (!didHit) return;

        Rotate(playerController.transform, hit.point);

        if (SwitchToMoveToAttackState(hit))
        {
            return;
        }

        playerController.Agent.SetDestination(hit.point);
        _indicator.SetActive(true);
        _indicator.transform.position = new Vector3(hit.point.x, 0.1f, hit.point.z);
    }

    private bool SwitchToMoveToAttackState(RaycastHit hit)
    {
        if (hit.transform.TryGetComponent(out IDamagable damagable))
        {
            stateManager.SwitchState(stateManager.MoveToAttackState);
            return true;
        }
        return false;
    }

    private void Rotate(Transform playerTransform, Vector3 mousePosition)
    {
        var direction = mousePosition - playerTransform.position;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        playerTransform.rotation = rotation;
    }
}
