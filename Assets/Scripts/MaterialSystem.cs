using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSystem : MonoSingleton<MaterialSystem>
{
    [SerializeField] List<Material> _pipeMaterials = new List<Material>();
    public void PipeDraw(ref GameObject pipe)
    {
        pipe.transform.GetChild(0).GetComponent<MeshRenderer>().material = _pipeMaterials[Random.Range(0, _pipeMaterials.Count)];
    }
}
