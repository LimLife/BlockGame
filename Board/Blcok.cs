using UnityEngine;
using System.Collections;

public class Blcok : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidBody;


    private Map _map;


    private void Start()
    {
        _map = FindObjectOfType<Map>();
        StartCoroutine(Faling());
    }

    private bool _isMove = true;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Ground>() || collision.gameObject.GetComponent<Blcok>()) // Заменить на условие Y <= 0
        {
            _isMove = false;
        }
    }
    private IEnumerator Faling()
    {
        while (_isMove)
        {
            yield return new WaitForSeconds(0.1f);
            if (_isMove)
            {
                transform.Translate(Vector3.down * 0.1f);
            }
            else
            {
                transform.parent = _map.transform;
                yield break;
            }
        }
    }
}
