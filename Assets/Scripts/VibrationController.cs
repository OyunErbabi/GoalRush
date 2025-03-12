using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lofelt.NiceVibrations;

public class VibrationController : MonoBehaviour
{
    public static VibrationController Instance;
    void Awake() { Instance = this; }

    bool hapticsSupported = false;
    private void Start()
    {
        hapticsSupported = DeviceCapabilities.isVersionSupported;
        ToggleVibration(true);
    }

    public void Vibrate(HapticPatterns.PresetType presetType)
    {
        if (hapticsSupported)
        {
            HapticPatterns.PlayPreset(presetType);
        }
    }

    public void ToggleVibration(bool status)
    {
        HapticController.hapticsEnabled = status;
    }
}
