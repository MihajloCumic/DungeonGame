using UnityEngine;

public class MoveState : State
{
    private const float threshold = 0.01f;
    public MoveState(StateManager stateManager) : base(stateManager)
    {
    }

    public override void ExitState()
    {
        return;
    }

    public override void CheckForChange()
    {
        if (stateManager.PlayerController.Agent.velocity.magnitude < threshold)
        {
            stateManager.PlayerController.Animator.CrossFade("Idle", 0.1f);
            stateManager.SwitchState(new IdleState(stateManager));
        }
    }

    public override void EnterState()
    {
        stateManager.PlayerController.Animator.CrossFade("Run", 0.1f);
        MoveAgent();
    }

    

    public override void UpdateState()
    {
        // if (Input.GetMouseButton(1))
        // {
        //     MoveAgent();
        // }
        return;
    }

    private void MoveAgent()
    {
        var mousePosition = Input.mousePosition;
        var playerController = stateManager.PlayerController;

        Ray ray = playerController.Cemara.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform.TryGetComponent(out PlayerController controller)) return;
            Rotate(playerController.transform, hit.point);
            playerController.Agent.SetDestination(hit.point);
        }
    }

    private void Rotate(Transform playerTransform, Vector3 mousePosition)
    {
        var direction = mousePosition - playerTransform.position;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        playerTransform.rotation = rotation;
    }
}
