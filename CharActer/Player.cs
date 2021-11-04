using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private BoardSize _boardSize;
    [SerializeField] private Camera _camera;
    [SerializeField] private Rigidbody2D _rigidBodyPlayer;

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private bool _isJump = true;
    private bool _isMove = true;

    private int Height => _boardSize.Height;
    private int Width => _boardSize.Width;

    private float _vector = 0.1f;

    private void Start()
    {
        transform.position = new Vector2((int)Random.Range(0, Width), 0);
    }

    private IEnumerator JumpFlag()
    {
        _rigidBodyPlayer.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
        yield return new WaitForSeconds(1);
        _isJump = true;
        yield break;

    }
    private void InputControl()
    {
        Vector2 pos = _camera.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) && _isMove == true)
        {
            if (transform.position.x < pos.x)
            {
                StartCoroutine(MoveDiraction(_vector));
                _isMove = false;
            }
            if (transform.position.x > pos.x)
            {
                StartCoroutine(MoveDiraction(-_vector));
                _isMove = false;
            }
            if ((int)transform.position.x == (int)pos.x)
            {
                return;
            }
        }
    }
    private IEnumerator MoveDiraction(float vector)
    {
        if (transform.position.x ==Width-1 || transform.position.x <=0)
        {
            transform.position = new Vector2(Mathf.Round(transform.position.x), transform.position.y);
            yield break;
        }
        byte iteration = 0;
        while (iteration < 10)
        {
            iteration++;
            yield return new WaitForSeconds(0.08f);
            transform.Translate(Vector2.right * vector);
        }
        _isMove = true;
        yield break;   
    }
    private void Jump()
    {
        if (_isJump && Input.GetMouseButtonDown(1))
        {
            StartCoroutine(JumpFlag());
            _isJump = false;
        }
    }
    private void Update()
    {
        InputControl();
        Jump();
    }
}
