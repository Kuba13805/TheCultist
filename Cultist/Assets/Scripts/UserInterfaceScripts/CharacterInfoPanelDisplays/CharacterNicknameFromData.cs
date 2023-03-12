using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CharacterNicknameFromData : MonoBehaviour
{
    public TMP_InputField inputField;
    public PlayerData playerData;
    // Start is called before the first frame update
    void Start()
    {
        inputField.text = playerData.nickname.ToString();
    }

}
