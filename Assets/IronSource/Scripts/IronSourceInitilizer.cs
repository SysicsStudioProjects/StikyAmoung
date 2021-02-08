﻿using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class IronSourceInitilizer
{
#if UNITY_IOS || UNITY_ANDROID
    [RuntimeInitializeOnLoadMethod]
    static void Initilize()
    {

        var developerSettings = Resources.Load<IronSourceMediationSettings>(IronSourceConstants.IRONSOURCE_MEDIATION_SETTING_NAME);
#if UNITY_ANDROID
        string appKey = developerSettings.AndroidAppKey;
#elif UNITY_IOS
        string appKey = developerSettings.IOSAppKey;
#endif
        if (developerSettings != null)
        {
            if (developerSettings.EnableIronsourceSDKInitAPI == true)
            {
                if (appKey.Equals(string.Empty))
                {
                    Debug.LogWarning("IronSourceInitilizer Cannot init without AppKey");
                }
                else
                {
                    IronSource.Agent.init(appKey);
                }

            }

            if (developerSettings.EnableAdapterDebug)
            {
                IronSource.Agent.setAdaptersDebug(true);
            }

            if (developerSettings.EnableIntegrationHelper)
            {
                IronSource.Agent.validateIntegration();
            }
        }
    }
#endif

}
