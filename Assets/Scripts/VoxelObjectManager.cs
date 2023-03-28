using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VoxelObjectManager : MonoSingleton<VoxelObjectManager>
{
    [SerializeField] List<GameObject> _VoxelObject = new List<GameObject>();
    public int voxelObjectCount, voxelObjectChildCount;
    GameObject _voxelMainObject;
    GameObject _thrash;
    [SerializeField] GameObject _voxelObjectPos;

    public void VoxelObjectManagerStart()
    {
        if (PlayerPrefs.HasKey("voxelObjectCount"))
            voxelObjectCount = PlayerPrefs.GetInt("voxelObjectCount");
        else
            PlayerPrefs.SetInt("voxelObjectCount", voxelObjectCount);
    }

    public void StartObjectPlacement()
    {
        _thrash = new GameObject("thrash");
        _voxelMainObject = Instantiate(_VoxelObject[voxelObjectCount % _VoxelObject.Count], _voxelObjectPos.transform.position, _voxelObjectPos.transform.rotation);
        voxelObjectChildCount = _voxelMainObject.transform.childCount;
    }

    public void ChildDown(GameObject stickman, float downChild)
    {
        Rigidbody rb = stickman.GetComponent<Rigidbody>();

        MoneySystem.Instance.MoneyTextRevork((int)downChild);
        stickman.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        stickman.SetActive(false);

        int count;
        for (int i = 0; i < (int)downChild; i++)
            if (_voxelMainObject.transform.childCount > 0)
            {
                _voxelMainObject.transform.GetChild(count = Random.Range(0, _voxelMainObject.transform.childCount)).GetComponent<Rigidbody>().isKinematic = false;
                _voxelMainObject.transform.GetChild(count).SetParent(_thrash.transform);
            }
            else NewVoxel();
    }
    private void NewVoxel()
    {
        _thrash.SetActive(false);
        voxelObjectCount++;
        VoxelObjectManagerStart();
        StartObjectPlacement();
    }
}
