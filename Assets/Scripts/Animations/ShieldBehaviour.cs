using Game.Player;

using UnityEngine;


namespace Game.Animations
{
    public class ShieldBehaviour : StateMachineBehaviour
    {
        private void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
            PlayerMovement player = GetPlayer(animator);
            if (player != null)
            {
                player.Anim_OnShieldArmed();
            }
        }

        private void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
            PlayerMovement player = GetPlayer(animator);
            if (player != null)
            {
                player.Anim_OnShieldUnarmed();
            }
        }

        private PlayerMovement GetPlayer(Animator animator)
        {
            return animator.gameObject.GetComponent<PlayerMovement>();
        }
    }
}

