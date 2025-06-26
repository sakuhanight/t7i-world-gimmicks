using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace T7i.WorldGimmicks
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class AnimationToggle : UdonSharpBehaviour
    {
        [Header("アニメーション設定")]
        [SerializeField] private Animator targetAnimator;
        [SerializeField] private string parameterName = "IsActive";

        [Header("初期値設定")]
        [SerializeField] private bool defaultValue = false;

        [UdonSynced] private bool isActive;

        void Start()
        {
            isActive = defaultValue;
            UpdateAnimator();
        }

        public override void Interact()
        {
            Networking.SetOwner(Networking.LocalPlayer, gameObject);
            isActive = !isActive;
            RequestSerialization();
            UpdateAnimator();
        }

        public void UpdateAnimator()
        {
            if (targetAnimator != null)
            {
                targetAnimator.SetBool(parameterName, isActive);
            }
        }

        public override void OnDeserialization()
        {
            UpdateAnimator();
        }

        public override void OnPlayerJoined(VRCPlayerApi player)
        {
            if (Networking.IsOwner(gameObject))
            {
                RequestSerialization();
            }
        }
    }
}