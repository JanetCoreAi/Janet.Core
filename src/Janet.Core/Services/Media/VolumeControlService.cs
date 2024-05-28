using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi;

namespace Janet.Core.Services.Media;

public interface IVolumeControlService
{
    void SetVolume(int volume);
    int GetVolume();
    void Mute();
    void Unmute();
    bool IsMuted();
    void IncreaseVolume();
    void DecreaseVolume();
}

public class VolumeControlService : IVolumeControlService
{
    [DllImport("user32.dll")]
    private static extern int SendMessageW(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

    private const uint WM_APPCOMMAND = 0x319;
    private const int APPCOMMAND_VOLUME_UP = 0xA0000;
    private const int APPCOMMAND_VOLUME_DOWN = 0x90000;
    private const int APPCOMMAND_VOLUME_MUTE = 0x80000;

    public void SetVolume(int volume)
    {
        if (volume < 0 || volume > 50)
            throw new ArgumentOutOfRangeException(nameof(volume), "Volume must be between 0 and 50.");

        var deviceEnumerator = new MMDeviceEnumerator();
        var device = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
        var volumeControl = device.AudioEndpointVolume;

        float targetVolume = volume / 50.0f;
        float currentVolume = volumeControl.MasterVolumeLevelScalar;

        // Fade to the new volume
        while (Math.Abs(currentVolume - targetVolume) > 0.01)
        {
            if (currentVolume < targetVolume)
                currentVolume += 0.01f;
            else
                currentVolume -= 0.01f;

            volumeControl.MasterVolumeLevelScalar = currentVolume;
            System.Threading.Thread.Sleep(10); // Adjust the sleep time for smoother or faster fading
        }

        volumeControl.MasterVolumeLevelScalar = targetVolume;
    }

    public int GetVolume()
    {
        var deviceEnumerator = new MMDeviceEnumerator();
        var device = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
        var volumeControl = device.AudioEndpointVolume;
        float volumeLevel = volumeControl.MasterVolumeLevelScalar;
        return (int)(volumeLevel * 50);
    }

    public void Mute()
    {
        var deviceEnumerator = new MMDeviceEnumerator();
        var device = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
        var volumeControl = device.AudioEndpointVolume;
        volumeControl.Mute = true;
    }

    public void Unmute()
    {
        var deviceEnumerator = new MMDeviceEnumerator();
        var device = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
        var volumeControl = device.AudioEndpointVolume;
        volumeControl.Mute = false;
    }

    public bool IsMuted()
    {
        var deviceEnumerator = new MMDeviceEnumerator();
        var device = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
        var volumeControl = device.AudioEndpointVolume;
        return volumeControl.Mute;
    }

    public void IncreaseVolume()
    {
        int currentVolume = GetVolume();
        if (currentVolume < 50)
        {
            SetVolume(currentVolume + 1);
        }
    }

    public void DecreaseVolume()
    {
        int currentVolume = GetVolume();
        if (currentVolume > 0)
        {
            SetVolume(currentVolume - 1);
        }
    }
}