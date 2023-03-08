using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapSystem : MonoSingleton<TapSystem>
{

    public IEnumerator TapStart()
    {
        while (GameManager.Instance.gameStat == GameManager.GameStat.start)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        TapMechanic();
                        break;

                }
                yield return new WaitForSeconds(Time.deltaTime);
            }
            yield return null;
        }
    }

    private void TapMechanic()
    {

    }
}
