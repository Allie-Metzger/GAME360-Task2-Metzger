using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;
using Debug = UnityEngine.Debug;

public class DashState : PlayerState
{
    public float dashDuration = 0.5f;
    public Vector2 dashDirection;
    private float dashTimeRemaining;
    float horizontalInput = Input.GetAxis("Horizontal");
        
    public override void EnterState(PlayerController player)
    {
        TryPlayAnimation(player, "Dash");

        dashTimeRemaining = player.dashDuration;

        bool wasInAir = !player.IsGrounded();

        float horizontalInput = Input.GetAxis("Horizontal");

        if (Mathf.Abs(horizontalInput) > 0.1f)
        {
            dashDirection = new Vector2(Mathf.Sign(horizontalInput), 0f);
        }
        else
        {
            dashDirection = new Vector2(player.spriteRenderer.flipX ? -1f : 1f, 0f);
        }

        Vector2 dashVelocity = dashDirection * player.dashSpeed;

        if (wasInAir)
        {
            float verticalVelocity = player.rb.linearVelocity.y * 0.5f;
            player.rb.linearVelocity = new Vector2(dashVelocity.x, verticalVelocity);
            Debug.Log("dash in air - vertical velocity");
        }
        else
        {
            player.rb.linearVelocity = new Vector2(dashVelocity.x, 0f);
        }

        player.rb.gravityScale = 3f;

        EventManager.TriggerEvent("OnPlayerDashed");

        if (AudioManager.Instance != null)
        {
            // AudioManager.Instance.PlayDashSound(); 
        }

    }
    private IEnumerator dashWait()
    {
        yield return new WaitForSeconds(dashDuration);

    }

    public override void UpdateState(PlayerController player)
    {
        dashTimeRemaining -= Time.deltaTime;
        Vector2 dashVelocity = dashDirection * player.dashSpeed;
        player.rb.linearVelocity = new Vector2(dashVelocity.x, player.rb.linearVelocity.y);

        if (dashTimeRemaining <= 0f)
        {
            if (player.IsGrounded())
            {
                float horizontal = Input.GetAxis("Horizontal");
                if (Mathf.Abs(horizontal) > 0.1f)
                {
                    player.ChangeState(new MovingState());
                }
                else
                {
                    player.ChangeState(new IdleState());
                }
            }
            else
            {
                player.ChangeState(new JumpingState());
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            player.Fire();
        }

    }

    public override void ExitState(PlayerController player)
    {
        Debug.Log("Exited Dashing State");
    }

    public override string GetStateName() => "Dashing";

    private void TryPlayAnimation(PlayerController player, string animName)
    {
        if (player.animator != null &&
            player.animator.runtimeAnimatorController != null &&
            player.animator.isActiveAndEnabled)
        {
            try
            {
                player.animator.Play(animName);
            }
            catch
            {
                // Animation doesn't exist - continue without it
            }
        }
    }
}