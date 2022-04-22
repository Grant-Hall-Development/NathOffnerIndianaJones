using UnityEngine;

public class MobileChecker : Singleton<MobileChecker>
{
    public bool isMobile;

    private void Awake()
    {
        base.Awake();
        isMobile = SystemInfo.deviceType == DeviceType.Handheld;
    }
}