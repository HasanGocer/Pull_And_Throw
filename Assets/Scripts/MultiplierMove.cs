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
    bool move;
    bool touchPlane;
    bool touchStartedOnPlayer;
    Touch touchFeyz;
    [SerializeField] MultiplierObject multiplierObject;

    void Start()
    {
        move = false;
        touchStartedOnPlayer = false;
    }

    private void OnMouseDown()
    {
        rb.isKinematic = false;
        touchStartedOnPlayer = true;
    }

    private void Update()
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
                                touchPlane = true;
                                GameObject newWayPoint = new GameObject("WayPoint");
                                newWayPoint.transform.position = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                                timer = 0;
                            }
                        }
                        break;
                    }
                case TouchPhase.Ended:
                    {
                        if (touchPlane == true)
                        {
                            touchStartedOnPlayer = false;
                            move = true;
                        }
                        break;
                    }
            }
        }

        if (move && GameManager.Instance.gameStat == GameManager.GameStat.start)
        {
            FinishMove();
        }
    }

    void FinishMove()
    {
        move = false;
        rb.isKinematic = true;

        if (multiplierObject.multiplierPosCount != -1)
        {
            transform.position = MultiplierSystem.Instance.multiplierStat.multiplierClass.multiplierPos[multiplierObject.multiplierPosCount].transform.position;
            MultiplierSystem.Instance.multiplierStat.multiplierClass.multiplierBool[multiplierObject.multiplierPosCount] = true;
            MultiplierSystem.Instance.multiplierStat.multiplierClass.multiplierCount[multiplierObject.multiplierPosCount] = multiplierObject.multiplierCount;
            MultiplierSystem.Instance.multiplierStat.multiplierClass.multiplierTypes[multiplierObject.multiplierPosCount] = multiplierObject.multiplierType;
        }
        else
        {

        }
    }
}