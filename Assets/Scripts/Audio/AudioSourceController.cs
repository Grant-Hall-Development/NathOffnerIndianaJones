using Sirenix.OdinInspector;
using UnityEngine;
namespace Base.Audio
{

    [RequireComponent(typeof(AudioSource))]
    public class AudioSourceController : MonoBehaviour
    {
        public AudioSource source;

        private void Awake()
        {
            FindReferences();
        }
        [Button("Find References!")]
        public void FindReferences()
        {
            source = GetComponent<AudioSource>();

        }
    }
}
