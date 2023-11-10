using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;

public class CollectingDataAgreement : MonoBehaviour
{
    [SerializeField] private GameObject agreementPanel;

    private string _consentStatusKey;

    private string _playerConsentKey;
    private async void Start()
    {
        await UnityServices.InitializeAsync();

        bool hasAskedForConsent = PlayerPrefs.HasKey(_consentStatusKey) && PlayerPrefs.GetInt(_consentStatusKey) == 1;

        bool hasPlayerAgreedToCollectData =
            PlayerPrefs.HasKey(_playerConsentKey) && PlayerPrefs.GetInt(_playerConsentKey) == 1;

        if (!hasAskedForConsent)
        {
            AskForConsent();
        }
        else switch (hasPlayerAgreedToCollectData)
        {
            case true:
                ConsentGiven();
                break;
            case false:
                ConsentDenied();
                break;
        }
    }
	
    private void AskForConsent()
    {
        PlayerPrefs.SetInt(_consentStatusKey, 1);
        PlayerPrefs.Save();
        
        agreementPanel.SetActive(true);
    }
	
    public void ConsentGiven()
    {
        AnalyticsService.Instance.StartDataCollection();
        
        PlayerPrefs.SetInt(_playerConsentKey, 1);
        PlayerPrefs.Save();
        
        agreementPanel.SetActive(false);
    }

    public void ConsentDenied()
    {
        PlayerPrefs.SetInt(_playerConsentKey, 0);
        PlayerPrefs.Save();
        
        agreementPanel.SetActive(false);
    }
}
