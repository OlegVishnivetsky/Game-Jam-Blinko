using OSSC;
using R3;
using Root;
using System;

namespace Services
{
    public class AudioService : IDisposable
    {
        private CompositeDisposable _disposables = new();
        private SoundController _controller;
        private GameSettings _gameSettings;
        private ISoundCue _musicCue;

        public PlaySoundSettings ActiveMusicSettings { get; private set; }

        public AudioService(SoundController controllerPrefab, GameSettings gameSettings)
        {
            _controller = UnityEngine.Object.Instantiate(controllerPrefab);
            UnityEngine.Object.DontDestroyOnLoad(_controller);
            _gameSettings = gameSettings;

            _gameSettings.MusicState.Subscribe(OnMusicStateChanged).AddTo(_disposables);
            _gameSettings.SoundState.Subscribe(OnSoundStateChanged).AddTo(_disposables);
        }

        public ISoundCue PlaySound(string name, ISoundCue cue = null)
        {
            PlaySoundSettings settings = new PlaySoundSettings();
            settings.Init();

            settings.name = name;
            settings.categoryName = "SFX";

            if (cue != null)
            {
                settings.soundCueProxy = cue;
            }

            return _controller.Play(settings);
        }

        public void PlayMusic(string name)
        {
            PlaySoundSettings settings = new PlaySoundSettings();
            settings.Init();

            settings.name = name;
            settings.categoryName = "Music";
            settings.isLooped = true;
            settings.fadeInTime = 1f;
            settings.fadeOutTime = 1f;

            ActiveMusicSettings = settings;

            StopMusic();
            _musicCue = _controller.Play(settings);
        }

        public void StopMusic()
        {
            if(_musicCue != null && _musicCue.IsPlaying) _musicCue?.Stop();
        }

        public void Dispose() => _disposables.Dispose();

        private void OnMusicStateChanged(bool state)
        {
            _controller.SetMute("Music", !state);
        }

        private void OnSoundStateChanged(bool state)
        {
            _controller.SetMute("SFX", !state);
        }
    }
}