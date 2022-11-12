using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private Targets targets;
    [SerializeField] private Animator anim;

    public void DisableTarget()
    {    
        anim.SetTrigger("isDestroyed");
 
    }

    public void ColorChange()
    {
        this.gameObject.SetActive(false);
        targets.RemoveGameobjectFromList(this.gameObject);
        targets.AddGameobjectToList(this.gameObject);
    }
}
