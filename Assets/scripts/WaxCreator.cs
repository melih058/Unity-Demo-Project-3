using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaxCreator : MonoBehaviour
{
    public GameObject waxPrefab;
    public int bodyLayerMask;
    public int waxLayerMask;
    public int hairLayerMask;
    public Transform waxParent;
    public Transform waxStick;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            createWax();
        }
        else if (Input.GetMouseButton(0))
        {
            createWax();
        }
    }

    private void createWax()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 50f, 1 << bodyLayerMask))
        {
            Vector3 pos = hit.point;
            waxStick.position = pos;
            waxStick.up = hit.normal;
            Collider[] colliders = Physics.OverlapSphere(pos, 0.015f, 1 << waxLayerMask);
            if (colliders.Length == 0)
            {
                Collider[] hairs = Physics.OverlapSphere(pos, 0.015f, 1 << hairLayerMask);
                for (int i = 0; i < hairs.Length; i++)
                {
                    hairs[i].transform.SetParent(waxParent);
                }
                Instantiate(waxPrefab, pos, Quaternion.identity, waxParent);
            }
        }
    }

    public void finish()
    {
        StartCoroutine(waxMoveRoutine());
    }

    private IEnumerator waxMoveRoutine()
    {
        float duration = 0f;
        while (duration < 2f)
        {
            waxParent.position += waxParent.right * Time.deltaTime * 5f;
            duration += Time.deltaTime;
            yield return null;
        }
    }
}
