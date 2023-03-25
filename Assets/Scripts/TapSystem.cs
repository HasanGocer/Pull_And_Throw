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
    private void StickmanAdd()
    {
        _stickmans.Add(ObjectPool.Instance.GetPooledObject(_OPStickmanCount, new Vector3(_stickmanPos.transform.position.x, _stickmanPos.transform.position.y, _stickmanPos.transform.position.z - _stickmans.Count * _sticmanDistance)));

    }

    private IEnumerator TapMechanic()
    {
        GameObject stickman = StickmanJump();
        StickmanAdd();
        yield return new WaitForSeconds(_upperCountdown);
        stickman.transform.position = _stickmanStartHitPos.transform.position;
    }

    private GameObject StickmanJump()
    {
        GameObject stickman = _stickmans[0];
        stickman.transform.DOJump(_stickmanFinishJumpPos.transform.position, 1, 1, _upperCountdown);
        _stickmans.RemoveAt(0);
        return stickman;
    }

}
