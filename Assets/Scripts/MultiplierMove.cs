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
    bool finishTime;
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
        finishTime = true;
        MultiplierSystem.Instance.isMove = true;
    }

    private void Update()
    {
        if (touchStartedOnPlayer)
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
                                Debug.DrawLine(Camera.main.transform.position, direction, Color.black, 1);
                                if (Physics.Raycast(Camera.main.transform.position, direction, out hit, 200f))
                                {
                                    transform.position = Vector3.Lerp(transform.position, new Vector3(hit.point.x, transform.position.y, hit.point.z), 10);
                                    timer = 0;
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
        else if (GameManager.Instance.gameStat == GameManager.GameStat.start && finishTime)
            FinishMove();
        MultiplierSystem.Instance.isMove = false;
    }

    void FinishMove()
    {
        MultiplierSystem multiplierSystem = MultiplierSystem.Instance;

        finishTime = false;
        rb.isKinematic = true;
        multiplierObject.multiplierCollider.isTrigger = true;

        if (multiplierObject.tempMultiplierPosCount != -1 && !multiplierSystem.multiplierStat.multiplierClass.multiplierBool[multiplierObject.tempMultiplierPosCount])
        {
            transform.position = multiplierSystem.multiplierStatPos[multiplierObject.tempMultiplierPosCount].transform.position;
            multiplierSystem.multiplierStat.multiplierClass.multiplierBool[multiplierObject.tempMultiplierPosCount] = true;
            multiplierSystem.multiplierStat.multiplierClass.multiplierCount[multiplierObject.tempMultiplierPosCount] = multiplierObject.multiplierCount;
            multiplierSystem.multiplierStat.multiplierClass.multiplierTypes[multiplierObject.tempMultiplierPosCount] = multiplierObject.multiplierType;

            if (multiplierObject.multiplierPosCount != -1)
            {
                multiplierSystem.multiplierStat.multiplierClass.multiplierBool[multiplierObject.multiplierPosCount] = false;
                multiplierObject.multiplierPosCount = -1;
            }
            else if (multiplierObject.multiplierMarketCount != -1)
            {
                multiplierSystem.multiplierStat.multiplierMarketClass.multiplierBool[multiplierObject.multiplierMarketCount] = false;
                multiplierObject.multiplierMarketCount = -1;
            }

            TapSystem.Instance.SetNewObjectCount();
            GameManager.Instance.MultiplierPlacementWrite(MultiplierSystem.Instance.multiplierStat);
        }
        else if (multiplierObject.multiplierPosCount != -1)
        {
            transform.position = multiplierSystem.multiplierStatPos[multiplierObject.multiplierPosCount].transform.position;
            multiplierSystem.multiplierStat.multiplierClass.multiplierBool[multiplierObject.multiplierPosCount] = true;
            multiplierSystem.multiplierStat.multiplierClass.multiplierCount[multiplierObject.multiplierPosCount] = multiplierObject.multiplierCount;
            multiplierSystem.multiplierStat.multiplierClass.multiplierTypes[multiplierObject.multiplierPosCount] = multiplierObject.multiplierType;
            multiplierSystem.multiplierStat.multiplierMarketClass.multiplierBool[multiplierObject.multiplierMarketCount] = false;

            TapSystem.Instance.SetNewObjectCount();
            GameManager.Instance.MultiplierPlacementWrite(MultiplierSystem.Instance.multiplierStat);
        }
        else
            transform.position = multiplierSystem.multiplierMarketPos[multiplierObject.multiplierMarketCount].transform.position;
    }
}