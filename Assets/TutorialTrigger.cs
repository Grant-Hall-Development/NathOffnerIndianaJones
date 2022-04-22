using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public TriggerDoFadeElements fadeElement;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        fadeElement.TriggerFadeIn();
    }
}
