using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private static readonly int HorizontalId = Animator.StringToHash("Horizontal");
    private static readonly int VerticalId = Animator.StringToHash("Vertical");
    private static readonly int SpeedId = Animator.StringToHash("Speed");
    private static readonly int FaceId = Animator.StringToHash("Face");

    [SerializeField]
    private float _moveSpeed = 1;

    [SerializeField]
    private Rigidbody2D _body;

    [SerializeField]
    private Animator _animator;

    private float _prevX;
    private float _prevY;

    private void Update()
    {
        MoveWithInput(Time.deltaTime);
    }

    private void MoveWithInput(float dt)
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        var movement = new Vector2(x * dt, y * dt).normalized * _moveSpeed;

        _animator.SetFloat(HorizontalId, x);
        _animator.SetFloat(VerticalId, y);
        _animator.SetFloat(SpeedId, movement.sqrMagnitude);

        _body.velocity = movement;

        if (Math.Abs(x) <= 0 && Math.Abs(y) <= 0
            && (Math.Abs(_prevX) > 0 || Math.Abs(_prevY) > 0))  
        {
            ChangeFace();
        }

        _prevX = x;
        _prevY = y;
    }

    private void ChangeFace()
    {
        if (Math.Abs(_prevX) > Math.Abs(_prevY))
        {
            _animator.SetFloat(FaceId, _prevX > 0 ? 1.1f : 0.1f);
        }
        else
        {
            _animator.SetFloat(FaceId, _prevY > 0 ? 2.1f : 3.1f);
        }

        _animator.Play("Idle");
        _prevY = 0;
        _prevY = 0;
    }
}