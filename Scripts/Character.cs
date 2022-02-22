using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _speed;
    [SerializeField] private float _startJumpForce;
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _targetDebugObj;

    private bool _isOnJump;
    private float _currentJumpForce;
    private bool _isDead;
    private float fallingAngle;

    private Vector3 GetAimingPoint()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        var height = ray.origin.y;
        var normalMultiplier = height / ray.direction.y;
        var rayVector = ray.direction * normalMultiplier;
        return ray.origin - rayVector;
    }

    private void Update()
    {
        if (_isDead)
        {
            return;
        }

        var inputDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        var _isMoving = inputDir.sqrMagnitude > float.Epsilon;
        var kill = Input.GetKeyDown(KeyCode.E);
        var isJumpRequested = Input.GetKeyDown(KeyCode.Space);
        var isDamageReceived = Input.GetKeyDown(KeyCode.Mouse1);
        var isAttacking = Input.GetKeyDown(KeyCode.Mouse0);
        var isFalling = Input.GetKeyDown(KeyCode.T);

        /*var aimPoint = GetAimingPoint();// + Vector3.up * 1.5f;
        _targetDebugObj.position = aimPoint;

        var angle = Vector3.SignedAngle(aimPoint - transform.position, inputDir, Vector3.up) + 180;
        _animator.SetFloat("AimingAngle", angle);*/
        if (kill)
        {
            _isDead = true;
            _animator.SetTrigger("Kill");
        }

        /*if (isFalling)
        {
            _animator.SetTrigger("Falling");
            _animator.SetFloat("FallingAngle", angle);
        }*/

        if (isAttacking)
        {
            _animator.SetTrigger("Attack");
        }

        if (_isMoving)
        {
            transform.position += inputDir.normalized * _speed * Time.deltaTime;
        }
        
        /*transform.forward = aimPoint - transform.position;*/

        if (isDamageReceived)
        {
            _animator.SetTrigger("DamageReceived");
        }

        if (isJumpRequested && !_isOnJump)
        {
            _isOnJump = true;
            _currentJumpForce = _startJumpForce;
        }

        var isOnAir = transform.position.y > 0;

        _animator.SetBool("isMoving", _isMoving);
        _animator.SetBool("IsOnAir", isOnAir);
    }

    private void LateUpdate()
    {
        if (!_isOnJump || _isDead)
        {
            return;
        }

        var curPos = transform.position;

        curPos.y += _currentJumpForce * Time.deltaTime;
        _currentJumpForce -= 9.8f * Time.deltaTime;
        if (curPos.y < 0)
        {
            curPos.y = 0;
        }

        transform.position = curPos;
        if (curPos.y == 0)
        {
            _isOnJump = false;
        }
    }
}