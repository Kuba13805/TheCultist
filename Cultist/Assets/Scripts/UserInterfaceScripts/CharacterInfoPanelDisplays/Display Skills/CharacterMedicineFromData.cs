using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CharacterMedicineFromData : MonoBehaviour
{
    public TMP_InputField inputField;
    public PlayerData playerData;
    // Start is called before the first frame update
    void Start()
    {
        inputField.text = playerData.medicine.ToString();
    }

}
