using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimatorData : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void Awake() =>
        _animator = GetComponent<Animator>();

    private void LogParameters()
    {
        bool isGrounded = _animator.GetBool(PlayerAnimatorData.Params.IsGrounded);
        float positionX = _animator.GetFloat(PlayerAnimatorData.Params.PositionX);
    }

    public void SetupIsGrounded(bool isGrounded) =>
        _animator.SetBool(PlayerAnimatorData.Params.IsGrounded, isGrounded);

    public void SetupPositionX(float positionX) =>
        _animator.SetFloat(PlayerAnimatorData.Params.PositionX, positionX); 
    
    public void SetupAttack(bool shouldAttack)
    {
        if (shouldAttack)
            _animator.SetTrigger(PlayerAnimatorData.Params.Attack);
    }

    public static class Params
    {
        public static readonly int IsGrounded = Animator.StringToHash(nameof(IsGrounded));
        public static readonly int PositionX = Animator.StringToHash(nameof(PositionX));
        public static readonly int Attack = Animator.StringToHash(nameof(Attack));
    }
}