using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour
{
    [SerializeField] Blcok _block;
    [SerializeField] BoardSize _size;

    private List<Blcok> block = new List<Blcok>();
    private int Height => _size.Height;
    private int Width => _size.Width;

    private Vector3 posRay;

   
    
    private void Start()
    {
        StartCoroutine(CheckOnFullRow());
    }

    

    private IEnumerator CheckOnFullRow()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            CrateRayCastCheck();
        }
    }
    /// <summary>
    /// Check evry 3 second Full row or no yet
    /// </summary>
    private void CrateRayCastCheck()
    {
        for (int i = 0; i < Height; i++)
        {
            posRay = new Vector3(0, i - 0.1f, 0);
            RaycastHit2D[] hits = Physics2D.RaycastAll(posRay, new Vector3(Width, i - 0.5f, 0));

            if (hits.Length == Width)
            {
                for (int element = 0; element < hits.Length; element++)
                {
                    block.Add(hits[element].collider.GetComponent<Blcok>());
                }
                OnDestroying(block);
                ChildrenTrnansleteRowDown();
            }
            else
            {
                return;
            }
        }
    }
    /// <summary>
    /// Delete row if is Full
    /// </summary>
    private void OnDestroying(List<Blcok> blcoks)
    {
        for (int i = 0; i < blcoks.Count; i++)
        {
            Destroy(blcoks[i].gameObject);
        }
    }
    /// <summary>
    /// Line down from which it is filled
    /// </summary>
    private void ChildrenTrnansleteRowDown()
    {
        // сделать через фор так как надо от высоты а не все Хотя тут всегда будет ряд 0 разрушатся
        foreach (Transform item in GetComponentInChildren<Transform>())
        {
            item.transform.Translate(Vector3Int.down);
        }
    }
}



