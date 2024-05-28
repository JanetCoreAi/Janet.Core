using Microsoft.Extensions.Logging;
using NAudio.Wave; // Add this using directive
using System;
using System.IO; // Add this using directive for DirectoryInfo and FileInfo
using Vlc.DotNet.Core;
using Vlc.DotNet.Core.Interops.Signatures;

namespace Janet.Core.Services.Media
{
    public interface IMediaPlayerService
    {
        void PlayAudio(string filePath);
        void StopAudio();
        void PlayVideo(string filePath);
        void StopVideo();
    }

    public class MediaPlayerService : IMediaPlayerService
    {
        private IWavePlayer waveOutDevice;
        private AudioFileReader audioFileReader;
        private VlcMediaPlayer vlcMediaPlayer;

        private ILogger<MediaPlayerService> logger;

        public MediaPlayerService(ILogger<MediaPlayerService> _logger)
        {
            logger = _logger;
            waveOutDevice = new WaveOut();
            vlcMediaPlayer = new VlcMediaPlayer(new DirectoryInfo(@"C:\Program Files\VideoLAN\VLC"));
        }

        public void PlayAudio(string filePath)
        {
            StopAudio();
            audioFileReader = new AudioFileReader(filePath);
            waveOutDevice.Init(audioFileReader);
            waveOutDevice.Play();
        }

        public void StopAudio()
        {
            if (waveOutDevice != null)
            {
                waveOutDevice.Stop();
                audioFileReader?.Dispose();
                audioFileReader = null;
            }
        }

        public void PlayVideo(string filePath)
        {
            StopVideo();
            vlcMediaPlayer.SetMedia(new FileInfo(filePath));
            vlcMediaPlayer.Play();
        }

        public void StopVideo()
        {
            if (vlcMediaPlayer != null)
            {
                vlcMediaPlayer.Stop();
            }
        }
    }
}

// Usage example in comments:
// IMediaPlayerService mediaPlayerService = new MediaPlayerService();
// mediaPlayerService.PlayAudio("path/to/audio/file.mp3");
// mediaPlayerService.StopAudio();
// mediaPlayerService.PlayVideo("path/to/video/file.mp4");
// mediaPlayerService.StopVideo();