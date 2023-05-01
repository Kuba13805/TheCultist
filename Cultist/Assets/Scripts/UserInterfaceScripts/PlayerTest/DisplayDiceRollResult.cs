using System.Collections;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayDiceRollResult : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resultTextBox;
    [SerializeField] private Image diceImage;
    [SerializeField] private GameObject dicePanel;

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
    }

    private void DisplayTestResult(int result)
    {
        dicePanel.SetActive(true);
        resultTextBox.text = result.ToString();
        StartCoroutine(WaitToDeactivate());
    }
    private void CheckIfTestWasPassed(bool test)
    {
        resultTextBox.color = test ? Color.green : Color.red;
    }

    private IEnumerator WaitToDeactivate()
    {
        yield return new WaitForSeconds(tweenDuration);
        dicePanel.SetActive(false);
    }
    
}
