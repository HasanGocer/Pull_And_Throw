using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody))]

public class MultiplierMove : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    public float timeForNextRay;
    float timer = 0;
    bool touchStartedOnPlayer;
    Touch touchFeyz;
    [SerializeField] MultiplierObject multiplierObject;

    void Start()
    {
        touchStartedOnPlayer = false;
    }

    private void OnMouseDown()
    {
        rb.isKinematic = false;
        touchStartedOnPlayer = true;
        multiplierObject.multiplierCollider.isTrigger = false;
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        yield return null;
        while (touchStartedOnPlayer)
        {
            timer += Time.deltaTime;
            if (Input.touchCount > 0 && GameManager.Instance.gameStat == GameManager.GameStat.start)
            {

                touchFeyz = Input.GetTouch(0);
                switch (touchFeyz.phase)
                {
                    case TouchPhase.Moved:
                        {
                            if (timer > timeForNextRay && touchStartedOnPlayer)
                            {
                                Vector3 worldFromMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100));
                                Vector3 direction = worldFromMousePos - Camera.main.transform.position;
                                RaycastHit hit;
                                if (Physics.Raycast(Camera.main.transform.position, direction, out hit, 100f))
                                {
                                    timer = 0;
                                    transform.position = Vector3.Lerp(transform.position, hit.transform.position, Time.deltaTime);
                                    yield return new WaitForSeconds(Time.deltaTime);
                                }
                            }
                            break;
                        }
                    case TouchPhase.Ended:
                        {
                            touchStartedOnPlayer = false;
                            break;
                        }
                }
            }
        }
        yield return new WaitForSeconds(0.3f);

        if (GameManager.Instance.gameStat == GameManager.GameStat.start)
            FinishMove();
    }

    void FinishMove()
    {
        rb.isKinematic = true;
        multiplierObject.multiplierCollider.isTrigger = true;

        if (multiplierObject.multiplierPosCount != -1)
        {
            transform.position = MultiplierSystem.Instance.multiplierStatPos[multiplierObject.multiplierPosCount].transform.position;
            MultiplierSystem.Instance.multiplierStat.multiplierClass.multiplierBool[multiplierObject.multiplierPosCount] = true;
            MultiplierSystem.Instance.multiplierStat.multiplierClass.multiplierCount[multiplierObject.multiplierPosCount] = multiplierObject.multiplierCount;
            MultiplierSystem.Instance.multiplierStat.multiplierClass.multiplierTypes[multiplierObject.multiplierPosCount] = multiplierObject.multiplierType;
            MultiplierSystem.Instance.multiplierStat.multiplierMarketClass.multiplierBool[multiplierObject.multiplierPosCount] = false;
        }
        else
            transform.position = MultiplierSystem.Instance.multiplierMarketPos[multiplierObject.multiplierPosCount].transform.position;
    }
}