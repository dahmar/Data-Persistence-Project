using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public void SetName(string name)
    {
        Persistence.Instance.currentName = name;
    }

    public void PlayClicked()
    {
        Persistence.Instance.OnPlayClicked();
    }
}
