using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteAlways]
public class NPCAnimationController : MonoBehaviour
{
    public Animator animator;

    public AnimationState animationToPlay;
    private AnimationState lastAnimation;

    public enum AnimationState
    {
        BreathingIdle,
        DwarfIdle,
        HangingIdle,
        Idle,
        DrunkIdleVariation,
        SadIdle,
        MaleSittingPose,
        FemaleSittingPose1,
        FemaleSittingPose2,
        Sitting1,
        Sitting2,
        Sitting3,
        Sitting4,
        SittingAndPointing,
        SittingIdle1,
        SittingIdle2,
        SittingIdle3,
        SittingRubbingArm,
        SittingTalking,
        SittingLaughing,
        WarriorIdle
    }

    private void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    private void OnValidate()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        PlayAnimation();
    }

    void Update()
    {
        if (animator == null) return;

        if (animationToPlay != lastAnimation)
        {
            PlayAnimation();
            lastAnimation = animationToPlay;
        }

#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            animator.Update(Time.deltaTime);
            SceneView.RepaintAll();
        }
#endif
    }

    void PlayAnimation()
    {
        string stateName = GetAnimationName(animationToPlay);
        animator.Play(stateName, 0, 0f);
    }

    string GetAnimationName(AnimationState state)
    {
        switch (state)
        {
            case AnimationState.BreathingIdle: return "Breathing Idle";
            case AnimationState.DwarfIdle: return "Dwarf Idle";
            case AnimationState.HangingIdle: return "Hanging Idle";
            case AnimationState.Idle: return "Idle";
            case AnimationState.DrunkIdleVariation: return "Drunk Idle Variation";
            case AnimationState.SadIdle: return "Sad Idle";
            case AnimationState.MaleSittingPose: return "Male Sitting Pose";
            case AnimationState.FemaleSittingPose1: return "Female Sitting Pose 1";
            case AnimationState.FemaleSittingPose2: return "Female Sitting Pose 2";
            case AnimationState.Sitting1: return "Sitting 1";
            case AnimationState.Sitting2: return "Sitting 2";
            case AnimationState.Sitting3: return "Sitting 3";
            case AnimationState.Sitting4: return "Sitting 4";
            case AnimationState.SittingAndPointing: return "Sitting And Pointing";
            case AnimationState.SittingIdle1: return "Sitting Idle 1";
            case AnimationState.SittingIdle2: return "Sitting Idle 2";
            case AnimationState.SittingIdle3: return "Sitting Idle 3";
            case AnimationState.SittingRubbingArm: return "Sitting Rubbing Arm";
            case AnimationState.SittingTalking: return "Sitting Talking";
            case AnimationState.SittingLaughing: return "Sitting Laughing";
            case AnimationState.WarriorIdle: return "Warrior Idle";
        }

        return "Idle";
    }
}