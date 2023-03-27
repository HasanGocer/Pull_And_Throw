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

    [Header("Tap_Field")]
    [Space(10)]

    [SerializeField] GameObject _stickmanFinishJumpPos;
    [SerializeField] GameObject _stickmanStartHitPos;
    [SerializeField] float _upperCountdown;
    [SerializeField] float _velocityPower;
    public float objectCount;

    public void Update()
    {
        if (GameManager.Instance.gameStat == GameManager.GameStat.start)
            if (Input.touchCount > 0)
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                    StartCoroutine(TapMechanic());
    }
    public void StickmanSort()
    {
        for (int i = 0; i < _stickmanCount; i++)
            _stickmans.Add(ObjectPool.Instance.GetPooledObject(_OPStickmanCount, new Vector3(_stickmanPos.transform.position.x, _stickmanPos.transform.position.y, _stickmanPos.transform.position.z - i * _sticmanDistance)));
    }
    public void SetNewObjectCount()
    {
        MultiplierSystem multiplierSystem = MultiplierSystem.Instance;
        ItemData itemData = ItemData.Instance;

        objectCount = 1;
        for (int i = 0; i < multiplierSystem.multiplierStat.multiplierClass.multiplierTypes.Count; i++)
            if (multiplierSystem.multiplierStat.multiplierClass.multiplierBool[i])
            {
                if (multiplierSystem.multiplierStat.multiplierClass.multiplierTypes[i] == MultiplierObject.MultiplierType.multiply) objectCount *= multiplierSystem.multiplierStat.multiplierClass.multiplierCount[i];
                else objectCount += multiplierSystem.multiplierStat.multiplierClass.multiplierCount[i];
            }
        objectCount += itemData.field.stickmanConstant + itemData.field.standartMoney;
    }

    private void StickmanMove()
    {
        for (int i = 0; i < _stickmans.Count; i++)
            StartCoroutine(StickmanMoveEnum(i));
    }
    private IEnumerator StickmanMoveEnum(int i)
    {
        _stickmans[i].transform.DOMove(new Vector3(_stickmanPos.transform.position.x, _stickmanPos.transform.position.y, _stickmanPos.transform.position.z - i * _sticmanDistance), 0.1f);
        yield return new WaitForSeconds(0.1f);
    }
    private void StickmanAdd()
    {
        _stickmans.Add(ObjectPool.Instance.GetPooledObjectAdd(_OPStickmanCount, new Vector3(_stickmanPos.transform.position.x, _stickmanPos.transform.position.y, _stickmanPos.transform.position.z - (_stickmans.Count + 1) * _sticmanDistance)));
        _stickmans[_stickmans.Count - 1].GetComponent<AnimController>().SkinOpen();
    }

    private IEnumerator TapMechanic()
    {
        GameObject stickman = StickmanJump();
        ObjectID objectID = stickman.GetComponent<ObjectID>();
        Rigidbody rb = stickman.GetComponent<Rigidbody>();

        StickmanAdd();
        StickmanMove();

        rb.isKinematic = false;
        yield return new WaitForSeconds(_upperCountdown);
        stickman.transform.position = _stickmanStartHitPos.transform.position;
        stickman.transform.rotation = Quaternion.Euler(new Vector3(stickman.transform.rotation.x + 270, stickman.transform.rotation.y, stickman.transform.rotation.z));
        objectID.rb.velocity = new Vector3(0, 0, _velocityPower);
    }

    private GameObject StickmanJump()
    {
        GameObject stickman = _stickmans[0];
        stickman.transform.DOJump(_stickmanFinishJumpPos.transform.position, 1, 1, _upperCountdown);
        _stickmans.RemoveAt(0);
        return stickman;
    }

}
