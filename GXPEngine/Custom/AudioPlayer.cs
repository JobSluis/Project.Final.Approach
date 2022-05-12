namespace GXPEngine.Custom
{
    public static class AudioPlayer
    {
        public static float volume = 1;


        public static void PlayAudio(string audio)
        {
            
            new Sound(audio).Play(volume: volume);
        }

        public static SoundChannel PlayAudioSong(string audio)
        {
            Sound s = new Sound(audio, true, true);
            return s.Play(volume: volume);
        }
    }
}