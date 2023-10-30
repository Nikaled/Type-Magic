using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsLoader : MonoBehaviour
{
    [SerializeField] TextFragments ClientTextFragment;
    [SerializeField] TypeSpeedSO TypeSpeedSettings;
    public readonly string SpeedPlayerPrefs = "SpeedPlayerPrefs";
    [SerializeField] Creator creator;
    [SerializeField] KillingPlatform killingPlatform;
    private bool IsChallengeMode;
    public void SetClientText()
    {
        if (ClientTextFragment != null)
        {
            creator.textfragments = ClientTextFragment;
        }
    }
    public void SetPlatformSettings()
    {
        int Speed = TypeSpeedSettings.TypeSpeed;
        IsChallengeMode = TypeSpeedSettings.IsChallengeMode;
        killingPlatform.SetPlatformSpeed(Speed, IsChallengeMode);
    }
}
