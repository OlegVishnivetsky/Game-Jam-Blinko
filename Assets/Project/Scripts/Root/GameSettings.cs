using R3;
using UnityEngine;
using Utils;

namespace Root
{
    public class GameSettings
    {
        public ReadOnlyReactiveProperty<bool> MusicState => _musicState;
        public ReadOnlyReactiveProperty<bool> SoundState => _soundState;

        private ReactiveProperty<bool> _musicState = new(true);
        private ReactiveProperty<bool> _soundState = new(true);


        public GameSettings()
        {
            LoadMusicState();
            LoadSoundState();
        }

        public void SetMusicState(bool isActive)
        {
            _musicState.Value = isActive;
            PlayerPrefs.SetInt(SaveKey.MUSIC_STATE_KEY, isActive ? 1 : 0);
        }
        private void LoadMusicState()
        {
            if (PlayerPrefs.HasKey(SaveKey.MUSIC_STATE_KEY))
            {
                var isActive = PlayerPrefs.GetInt(SaveKey.MUSIC_STATE_KEY) == 1;
                _musicState.Value = isActive;
            }
        }

        public void SetSoundState(bool isActive)
        {
            _soundState.Value = isActive;
            PlayerPrefs.SetInt(SaveKey.SOUND_STATE_KEY, isActive ? 1 : 0);

        }
        private void LoadSoundState()
        {
            if (PlayerPrefs.HasKey(SaveKey.SOUND_STATE_KEY))
            {
                var isActive = PlayerPrefs.GetInt(SaveKey.MUSIC_STATE_KEY) == 1;
                _soundState.Value = isActive;
            }
        }
    }
}