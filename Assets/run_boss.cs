using UnityEngine;

public class run_boss : StateMachineBehaviour
{
    private Transform playerTransform;
    private BossFollowPlayer bossScript;
    private float stoppingDistance;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossScript = animator.GetComponent<BossFollowPlayer>();
        if (bossScript != null)
        {
            playerTransform = bossScript.playerTransform;
            stoppingDistance = bossScript.stoppingDistance;
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playerTransform == null || bossScript == null) return;

        float distanceToPlayer = Vector2.Distance(animator.transform.position, playerTransform.position);

        // Jika saat berlari jarak sudah sangat dekat dengan player
        if (distanceToPlayer <= stoppingDistance)
        {
            // Matikan isRunning agar transisi dari running -> basicAttack (atau ke idle) bisa terbuka
            animator.SetBool("isRunning", false);
        }
    }
}