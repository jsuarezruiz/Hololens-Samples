using System.Runtime.Serialization;
using WaveEngine.Framework;
using WaveEngine.Framework.Services;
using WaveEngine.Framework.Sound;
using WaveEngine.Hololens.Speech;

namespace HoloPlanets
{
    [DataContract]
    public class VoiceCommands : Component
    {
        private const string MusicTag = "Music";
        private const string TurnMusicOn = "Turn Music On";
        private const string TurnMusicOff = "Turn Music Off";

        private KeywordRecognizerService _keywordService;

        protected override void DefaultValues()
        {
            base.DefaultValues();
        }

        protected override void Initialize()
        {
            base.Initialize();

            _keywordService = WaveServices.GetService<KeywordRecognizerService>();

            if (_keywordService != null)
            {
                _keywordService.Keywords = new string[] { TurnMusicOn, TurnMusicOff };
                _keywordService.Start();
                _keywordService.OnKeywordRecognized += OnKeywordRecognized;
            }
        }

        private void OnKeywordRecognized
            (KeywordRecognizerResult result)
        {
            switch (result.Text)
            {
                case TurnMusicOn:
                    TurnMusic(true);
                    break;
                case TurnMusicOff:
                    TurnMusic(false);
                    break;
            }
        }

        private void TurnMusic(bool state)
        {
            foreach (Entity soundEntity in this.Owner.FindAllChildrenByTag(MusicTag))
            {
                var emitter = soundEntity.FindComponent<SoundEmitter3D>();

                if (state)
                {
                    emitter.Play();
                    emitter.PlayAutomatically = true;
                }
                else
                {
                    emitter.PlayAutomatically = false;
                    WaveServices.SoundPlayer.StopAllSounds();
                }
            }
        }
    }
}