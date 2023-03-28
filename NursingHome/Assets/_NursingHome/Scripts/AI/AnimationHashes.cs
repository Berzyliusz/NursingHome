using UnityEngine;

namespace NursingHome.Animations
{
    public static class AnimationHashes
    {
        public static int WalkHash = Animator.StringToHash("Walk");
        public static int IdleHash = Animator.StringToHash("Idle");
        public static int WorkHash = Animator.StringToHash("Work");
        public static int ChaseHash = Animator.StringToHash("Chase");
        public static int SitHash = Animator.StringToHash("Sit");
        public static int LayHash = Animator.StringToHash("Lay");
    }
}