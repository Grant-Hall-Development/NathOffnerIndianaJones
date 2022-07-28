using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableIfPC : MonoBehaviour
{
    public void Update()
    {
        gameObject.SetActive(MobileChecker.Instance.isMobile);
    }

}
