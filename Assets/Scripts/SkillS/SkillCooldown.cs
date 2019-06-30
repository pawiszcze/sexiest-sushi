using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCooldown : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<Image>().type = Image.Type.Filled;
    }    
}
