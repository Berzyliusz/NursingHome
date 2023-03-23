namespace BehaviorDesigner.Runtime.Tasks
{
    [TaskDescription("A normal sequence having different icon for readability. The sequence task is similar to an \"and\" operation. It will return failure as soon as one of its child tasks return failure. " +
                 "If a child task returns success then it will sequentially run the next task. If all child tasks return success then it will return success.")]
    [TaskIcon("Assets/Gizmos/BTGizmos/system-icon_enter-and-play-dialogue_sequence2.png")]
    [TaskCategory("Woodland")]
    public class DialogueSequence : Sequence
    {

    }
}