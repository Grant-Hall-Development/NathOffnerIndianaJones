using System.Linq;
using UnityEngine;
namespace Base.Audio
{
    [CreateAssetMenu(fileName = "AllAudioClipsManager", menuName = "CustomAssets/Audio/Create AudioClips Manager")]
    public class AllAudioSO : ScriptableObject
    {
        public AudioClipTemplate[] allMusicAudio;
        public AudioClipTemplate[] allSFXAudio;

        public AudioClipTemplate GetAudioClipFromCollection(AudioClipTemplate[] collection, string name)
        {
            return collection.ToList().FirstOrDefault(t => t.audioName == name);
        }
    }

}