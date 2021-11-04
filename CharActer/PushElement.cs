using System.Collections;
using UnityEngine;

public class PushElement : MonoBehaviour
{
    [SerializeField] private GameObject _select = null;

    [SerializeField] private BoardSize _board;

    private int Withd => _board.Width;
    private float _offsetStartRay = 0.5f;
    private float _offsetEndRay = 0.1f;
    private float _distanse = 0.5f;
    private float _diractonValue = 0.1f;
    private bool _push = true;
    private bool _freeUpSpase = false;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Blcok>())
        {
            _select = collision.gameObject;
            if (_select.transform.position.x == 0 || _select.transform.position.x == Withd - 1)
            {
                _select = null;
            }
            if (_select != null)
            {
                CheckFreePushPostion(_select);
            }
        }
    }
    private void Update()
    {
        if (_select != null)
        {
            Check(_select);
        }
    }
    private void Check(GameObject sel)
    {
        Debug.DrawLine(
          new Vector2(sel.transform.position.x - _offsetStartRay, sel.transform.position.y),
          new Vector2(sel.transform.position.x - _offsetEndRay, sel.transform.position.y), Color.red);
    }
    private void CheckFreePushPostion(GameObject selectColltion)
    {
        Vector2 selectObj = new Vector2(selectColltion.transform.position.x, selectColltion.transform.position.y);

        RaycastHit2D spaseRight = Physics2D.Raycast(
             new Vector2(selectObj.x + _offsetStartRay, selectObj.y),
             new Vector2(selectObj.x + _offsetEndRay, selectObj.y), _distanse);

        RaycastHit2D spaseLeftt = Physics2D.Raycast(
             new Vector2(selectObj.x - _offsetStartRay * 2, selectObj.y),
             new Vector2(selectObj.x - _offsetEndRay * 2, selectObj.y), _distanse);   // I dno`t know way need multiply x2 ray Fixxxxxx

        RaycastHit2D spaseUp = Physics2D.Raycast(
            new Vector2(selectObj.x, selectObj.y + _offsetStartRay),
            selectObj, _distanse);

        if (spaseRight == gameObject || spaseLeftt == gameObject)
        {
            _select = null;
        }

        if (spaseUp != gameObject)
        {
            _freeUpSpase = true;
        }

        if (transform.position.x > selectColltion.transform.position.x)
        {
            if (spaseLeftt != gameObject && _freeUpSpase == true)
            {
                if (_push == true)
                {
                    StartCoroutine(Push(selectColltion, -_diractonValue));
                    _select = null;
                    _push = false;
                    if (CheckBottom(selectColltion))
                    {
                        StartCoroutine(Falling(selectColltion)); 
                    }
                }
                _freeUpSpase = false;
            }
        }

        if (transform.position.x < selectColltion.transform.position.x)
        {
            if (spaseRight != gameObject && _freeUpSpase == true)
            {
                if (_push == true)
                {
                    StartCoroutine(Push(selectColltion, _diractonValue));
                    _select = null;
                    _push = false;
                    if (CheckBottom(selectColltion))
                    {
                        StartCoroutine(Falling(selectColltion));
                    }
                }
                _freeUpSpase = false;
            }
        }
    }
    private bool CheckBottom(GameObject gameObject)
    {
        RaycastHit2D botoom = Physics2D.Raycast(
           new Vector2(gameObject.transform.position.x, transform.position.y),
           new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + _offsetEndRay), _distanse);
        if (botoom != gameObject)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private IEnumerator Push(GameObject gameObject, float dir)
    {
        byte iterate = 0;
        while (iterate < 10)
        {
            iterate++;
            yield return new WaitForSeconds(0.1f);
            gameObject.transform.Translate(Vector2.right * dir);
        }
        gameObject.transform.position = new Vector2(Mathf.Round(gameObject.transform.position.x), gameObject.transform.position.y);
        _push = true;
        yield break;
    }

    private IEnumerator Falling(GameObject gameObject)
    {
        byte iterate = 0;
        while (iterate < 10)
        {
            iterate++;
            gameObject.transform.Translate(Vector2.down * _diractonValue);
        }

        yield break;
    }

}
