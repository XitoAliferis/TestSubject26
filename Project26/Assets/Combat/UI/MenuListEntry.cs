using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuListEntry : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text costValueText;
    public TMP_Text costTypeText;

    public void Set(string name, int costValue) {
        nameText.text = name;
        costValueText.text = costValue.ToString();
    }
}
