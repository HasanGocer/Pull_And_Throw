using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelTouch : MonoSingleton<VoxelTouch>
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Object"))
            VoxelObjectManager.Instance.ChildDown(gameObject, TapSystem.Instance.objectCount);
    }
}
