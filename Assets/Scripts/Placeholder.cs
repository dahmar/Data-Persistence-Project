using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Placeholder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var name = Persistence.Instance.currentName;
        if (name.Length > 0)
        {
            GetComponent<TextMeshProUGUI>().text = name;
        }
    }
}
