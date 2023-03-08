using UnityEngine;
using Random = UnityEngine.Random;

namespace _ProjectSurvival.Scripts.Temp
{
    public class RandomBGImagePicker : MonoBehaviour
    {
        [SerializeField] private Sprite[] _bgSprites;
    
        private SpriteRenderer _bg;

        private void Start()
        {
            _bg = GetComponent<SpriteRenderer>();
            _bg.sprite = _bgSprites[Random.Range(0, _bgSprites.Length)];
        }
    }
}
