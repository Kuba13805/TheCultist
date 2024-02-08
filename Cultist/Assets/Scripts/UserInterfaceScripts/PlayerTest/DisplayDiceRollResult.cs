using System.Collections;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayDiceRollResult : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI diceResultTextBox;
    [SerializeField] private Image diceImage;
    [SerializeField] private GameObject dicePanel;
    [SerializeField] private TextMeshProUGUI textResultTextBox;

    [SerializeField] private float tweenDuration;
    [SerializeField] private Vector3 scaleTween;
    private void Start()
    {
        TestPlayer.OnDiceRoll += DisplayTestResult;

        GameManager.OnPlayerTestCheck += CheckIfTestWasPassed;
        
        dicePanel.SetActive(false);
    }


    private void OnDisable()
    {
        TestPlayer.OnDiceRoll -= DisplayTestResult;
        
        GameManager.OnPlayerTestCheck -= CheckIfTestWasPassed;
    }

    private void DisplayTestResult(int result)
    {
        dicePanel.SetActive(true);
        diceResultTextBox.text = result.ToString();
        StartCoroutine(WaitToDeactivate());
    }
    private void CheckIfTestWasPassed(bool test)
    {
        diceResultTextBox.color = test ? Color.green : Color.red;
        
        textResultTextBox.color = test ? Color.green : Color.red;

        textResultTextBox.text = test ? "Test passed!" : "Test failed!";
    }

    private IEnumerator WaitToDeactivate()
    {
        yield return new WaitForSeconds(tweenDuration);
        dicePanel.SetActive(false);
    }
    
}
