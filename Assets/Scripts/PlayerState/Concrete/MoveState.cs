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
        //Ovde je problem sa animacijom trcanja kada se krene iz mesta
        if (stateManager.PlayerController.Agent.velocity.magnitude < threshold)
        {
            stateManager.SwitchState(new IdleState(stateManager));
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
        playerController.Agent.SetDestination(hit.point);
    }

    private void Rotate(Transform playerTransform, Vector3 mousePosition)
    {
        var direction = mousePosition - playerTransform.position;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        playerTransform.rotation = rotation;
    }
}
