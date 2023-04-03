using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;

public class AnimController : MonoBehaviour
{
    public List<AnimancerComponent> characters = new List<AnimancerComponent>();
    [SerializeField] private AnimationClip walk, death, ýdle;
    [SerializeField] GameObject _fire;

    public void CallIdleAnim()
    {
        characters[ItemData.Instance.field.stickmanConstant].Play(ýdle, 0.2f);
    }
    public void CallDeadAnim()
    {
        characters[ItemData.Instance.field.stickmanConstant].Play(death, 0.2f);
    }
    public void CallWalkAnim()
    {
        characters[ItemData.Instance.field.stickmanConstant].Play(walk, 0.2f);
    }
    public void SkinOpen()
    {
        foreach (AnimancerComponent item in characters) item.gameObject.SetActive(false);
        characters[ItemData.Instance.field.stickmanConstant].gameObject.SetActive(true);
        _fire.SetActive(false);
    }
    public void FireOpen(bool isTouch)
    {
        if (ItemData.Instance.field.stickmanConstant > 4 || isTouch) _fire.SetActive(true);
    }
}
