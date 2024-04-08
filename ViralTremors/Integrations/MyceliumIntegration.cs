﻿using System.Runtime.CompilerServices;
using ViralTremors.Buttplug;
using ViralTremors.Comments.Events;

namespace ViralTremors.Integrations;

public class MyceliumIntegration
{
    private static bool? _enabled;

    public static bool enabled
    {
        get
        {
            _enabled ??= BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("RugbugRedfern.MyceliumNetworking");

            return (bool)_enabled;
        }
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    public static void InitializeIntegration()
    {
        ViralTremors.Logger.LogInfo("Initializing MyceliumNetworking integration");

        ViralTremors.DeviceManager.OnVibrated += DeviceManagerOnOnVibrated;
        InitializeHooks();
    }

    private static void InitializeHooks()
    {
        On.ContentEventIDMapper.GetContentEvent += ContentEventIDMapperOnGetContentEvent;
    }

    private static ContentEvent ContentEventIDMapperOnGetContentEvent(On.ContentEventIDMapper.orig_GetContentEvent orig,
        ushort id)
    {
        return id switch
        {
            5601 => new VibeContentEvent(),
            _ => orig(id)
        };
    }

    private static void DeviceManagerOnOnVibrated(object sender, VibratedEventArgs e)
    {
        throw new System.NotImplementedException();
    }
}