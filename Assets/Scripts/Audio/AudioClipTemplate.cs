using UnityEngine;
using Sirenix.OdinInspector;

namespace Base.Audio
{
    [CreateAssetMenu(fileName = "Custom_AudioClip", menuName = "CustomAssets/Audio/Create New Audio Template")]
    [System.Serializable]
    public class AudioClipTemplate : ScriptableObject
    {
        public string audioName;
        public AudioClip clip;
        public bool isLooping;
        public bool randomisePitch;
        [Range(0f, 1f)]
        public float desiredVolume;
        [Button("Test")]
        public void TestSFX()
        {
            AudioManager.Instance.PlaySFX(this);
        }
    }

}