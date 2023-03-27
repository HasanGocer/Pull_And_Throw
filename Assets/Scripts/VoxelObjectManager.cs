using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VoxelObjectManager : MonoSingleton<VoxelObjectManager>
{
    [SerializeField] List<GameObject> _VoxelObject = new List<GameObject>();
    public int voxelObjectCount, voxelObjectChildCount;
    GameObject _voxelMainObject;
    [SerializeField] GameObject _voxelObjectPos, _downPos;

    public void VoxelObjectManagerStart()
    {
        if (PlayerPrefs.HasKey("voxelObjectCount"))
            voxelObjectCount = PlayerPrefs.GetInt("voxelObjectCount");
        else
            PlayerPrefs.SetInt("voxelObjectCount", voxelObjectCount);
    }

    public void StartObjectPlacement()
    {
        _voxelMainObject = Instantiate(_VoxelObject[voxelObjectCount], _voxelObjectPos.transform.position, _voxelObjectPos.transform.rotation);
        voxelObjectChildCount = _voxelMainObject.transform.childCount;
    }

    public void ChildDown(GameObject stickman, int downChild)
    {
        TapSystem.Instance.StickmanBackAdded(stickman);
        int count;
        for (int i = 0; i < downChild; i++)
        {

            _voxelMainObject.transform.GetChild(count = Random.Range(0, _voxelMainObject.transform.childCount)).GetComponent<Rigidbody>().isKinematic = false;
            _voxelMainObject.transform.GetChild(count).SetParent(null);
        }
    }

}
