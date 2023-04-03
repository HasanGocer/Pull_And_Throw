using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TapSystem : MonoSingleton<TapSystem>
{
    [Header("Stickman_Field")]
    [Space(10)]


    [SerializeField] int _OPStickmanCount;
    [SerializeField] List<GameObject> _stickmans = new List<GameObject>();
    [SerializeField] GameObject _stickmanPos;
    [SerializeField] float _sticmanDistance;
    [SerializeField] int _stickmanCount;
    bool isTap;

    [Header("Tap_Field")]
    [Space(10)]

    [SerializeField] GameObject _stickmanFinishJumpPos;
    [SerializeField] GameObject _stickmanStartHitPos;
    [SerializeField] float _upperCountdown;
    [SerializeField] float _velocityPower;
    public float objectCount;

    public void Update()
    {
        if (GameManager.Instance.gameStat == GameManager.GameStat.start && !MultiplierSystem.Instance.isMove)
            if (Input.touchCount > 0)
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                    StartCoroutine(TapMechanic(true));
    }
    public void StickmanOff()
    {
        foreach (GameObject item in _stickmans) item.SetActive(false);
    }
    public void StickmanOn()
    {
        foreach (GameObject item in _stickmans) item.SetActive(true);
    }
    public void StickmanSort()
    {
        for (int i = 0; i < _stickmanCount; i++)
        {
            _stickmans.Add(ObjectPool.Instance.GetPooledObject(_OPStickmanCount, new Vector3(_stickmanPos.transform.position.x, _stickmanPos.transform.position.y, _stickmanPos.transform.position.z - i * _sticmanDistance)));
            _stickmans[_stickmans.Count - 1].GetComponent<AnimController>().SkinOpen();
        }
    }
    public IEnumerator AuotShot()
    {
        while (true)
        {
            if (GameManager.Instance.gameStat == GameManager.GameStat.start)
            {
                StartCoroutine(TapMechanic(false));
                yield return new WaitForSeconds(ItemData.Instance.field.shotCountdown);
            }
            yield return null;
        }
    }
    public void SetNewObjectCount()
    {
        MultiplierSystem multiplierSystem = MultiplierSystem.Instance;
        ItemData itemData = ItemData.Instance;

        objectCount = itemData.field.standartMoney;
        for (int i = 0; i < multiplierSystem.multiplierStat.multiplierClass.multiplierTypes.Count; i++)
            if (multiplierSystem.multiplierStat.multiplierClass.multiplierBool[i])
            {
                if (multiplierSystem.multiplierStat.multiplierClass.multiplierTypes[i] == MultiplierObject.MultiplierType.multiply) objectCount *= multiplierSystem.multiplierStat.multiplierClass.multiplierCount[i];
                else objectCount += multiplierSystem.multiplierStat.multiplierClass.multiplierCount[i];
            }
        objectCount += itemData.field.stickmanConstant;
    }

    private void StickmanMove()
    {
        for (int i = 0; i < _stickmans.Count; i++)
            StartCoroutine(StickmanMoveEnum(i));
    }
    private IEnumerator StickmanMoveEnum(int i)
    {
        _stickmans[i].transform.DOMove(new Vector3(_stickmanPos.transform.position.x, _stickmanPos.transform.position.y, _stickmanPos.transform.position.z + i * _sticmanDistance), 0.1f);
        yield return new WaitForSeconds(0.1f);
    }
    private void StickmanAdd()
    {
        _stickmans.Add(ObjectPool.Instance.GetPooledObjectAdd(_OPStickmanCount, new Vector3(_stickmanPos.transform.position.x, _stickmanPos.transform.position.y, _stickmanPos.transform.position.z + (_stickmans.Count + 1) * _sticmanDistance)));
        _stickmans[_stickmans.Count - 1].GetComponent<AnimController>().SkinOpen();
    }

    private IEnumerator TapMechanic(bool isTouch)
    {
        if (!isTap)
        {
            isTap = true;

            GameObject stickman = StickmanJump();
            ObjectID objectID = stickman.GetComponent<ObjectID>();
            Rigidbody rb = stickman.GetComponent<Rigidbody>();

            StickmanAdd();
            StickmanMove();
            stickman.transform.DORotate(new Vector3(90, 180, 0), _upperCountdown);
            rb.isKinematic = false;
            yield return new WaitForSeconds(_upperCountdown);
            stickman.transform.position = _stickmanStartHitPos.transform.position;
            stickman.transform.rotation = Quaternion.Euler(new Vector3(stickman.transform.rotation.x + 90, stickman.transform.rotation.y, stickman.transform.rotation.z));
            objectID.rb.velocity = new Vector3(0, 0, _velocityPower);
            if (isTouch)
                objectID.rb.velocity += new Vector3(0, 0, 10);
            stickman.GetComponent<AnimController>().FireOpen(isTouch);

            isTap = false;
        }
    }

    private GameObject StickmanJump()
    {
        GameObject stickman = _stickmans[0];
        stickman.transform.DOMove(_stickmanFinishJumpPos.transform.position, _upperCountdown);
        _stickmans.RemoveAt(0);
        return stickman;
    }

}
