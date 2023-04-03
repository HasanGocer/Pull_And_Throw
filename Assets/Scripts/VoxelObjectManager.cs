using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class VoxelObjectManager : MonoSingleton<VoxelObjectManager>
{
    [Header("Voxel_Panel")]
    [Space(10)]

    [SerializeField] List<GameObject> _VoxelObject = new List<GameObject>();
    public int voxelObjectChildCount;
    GameObject _voxelMainObject;
    GameObject _thrash;
    [SerializeField] GameObject _voxelObjectPos;

    [Header("Voxel_UI_Panel")]
    [Space(10)]

    [SerializeField] GameObject _barPanel;
    [SerializeField] Image _bar;
    [SerializeField] int _maxChildCount;

    public void StartObjectPlacement()
    {
        _thrash = new GameObject("thrash");
        _voxelMainObject = Instantiate(_VoxelObject[(GameManager.Instance.level - 1) % _VoxelObject.Count], _voxelObjectPos.transform.position, _voxelObjectPos.transform.rotation);
        voxelObjectChildCount = _voxelMainObject.transform.childCount;
        _maxChildCount = voxelObjectChildCount;
    }
    public void VoxelOff()
    {
        _voxelMainObject.SetActive(false);
        _barPanel.SetActive(false);
    }
    public void VoxelOn()
    {
        _voxelMainObject.SetActive(true);
        _barPanel.SetActive(true);
    }
    public void ChildDown(GameObject stickman, float downChild)
    {
        Rigidbody rb = stickman.GetComponent<Rigidbody>();

        downChild /= GameManager.Instance.level;

        MoneySystem.Instance.MoneyTextRevork((int)downChild);
        stickman.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        stickman.SetActive(false);
        ParticalManager.Instance.CallCoinPartical(_voxelMainObject);
        Vibration.Vibrate(30);
        SoundSystem.Instance.CallCoinSound();

        int count;
        for (int i = 0; i < (int)downChild; i++)
            if (_voxelMainObject.transform.childCount > 0)
            {
                _voxelMainObject.transform.GetChild(count = Random.Range(0, _voxelMainObject.transform.childCount)).GetComponent<Rigidbody>().isKinematic = false;
                _voxelMainObject.transform.GetChild(count).SetParent(_thrash.transform);
                StartCoroutine(BarUpdate());
            }
            else
            {
                NewVoxel();
                continue;
            }
    }


    private IEnumerator BarUpdate()
    {
        while (true)
        {
            _bar.fillAmount = Mathf.Lerp(_bar.fillAmount, ((float)_voxelMainObject.transform.childCount) / (float)_maxChildCount, Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
            if (_bar.fillAmount == ((float)_voxelMainObject.transform.childCount) / (float)_maxChildCount) break;
        }
    }
    private void NewVoxel()
    {
        _thrash.SetActive(false);
        GameManager.Instance.SetLevel();
        GameManager.Instance.gameStat = GameManager.GameStat.finish;
        Buttons.Instance.winPanel.SetActive(true);
    }
}
