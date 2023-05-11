using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Threading.Tasks;
using _ProjectSurvival.Scripts.AI;
using _ProjectSurvival.Scripts.Enemies;
using UnityEditor;

namespace OpenAI
{
    public class DallE : MonoBehaviour
    {
        [SerializeField] private InputField _inputField;
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;
        [SerializeField] private GameObject _loadingLabel;
        [SerializeField] private MonsterTypesSO _monsterTypesSO;

        private OpenAIApi openai = new OpenAIApi();

        private void Start()
        {
            _button.onClick.AddListener(SendImageRequest);
        }

        private async void SendImageRequest()
        {
            _image.sprite = null;
            _button.enabled = false;
            _inputField.enabled = false;
            _loadingLabel.SetActive(true);
            
            var response = await openai.CreateImage(new CreateImageRequest
            {
                Prompt = _inputField.text + "white background, fullsize, in the middle, single image",
                Size = ImageSize.Size256,
            });

            if (response.Data != null && response.Data.Count > 0)
            {
                using(var request = new UnityWebRequest(response.Data[0].Url))
                {
                    request.downloadHandler = new DownloadHandlerBuffer();
                    request.SetRequestHeader("Access-Control-Allow-Origin", "*");
                    request.SendWebRequest();

                    while (!request.isDone) await Task.Yield();

                    Texture2D texture = new Texture2D(2, 2);
                    texture.LoadImage(request.downloadHandler.data);

                    //Color black = new Color(0, 0, 0);
                    //Color white = new Color(1, 1, 1);
                    
                    float brightnessThreshold = 0.78f;
                    for (int i = 0; i < texture.width; i++)
                    {
                        for (int j = 0; j < texture.height; j++)
                        {
                            Color pixel = texture.GetPixel(i, j);
                            float brightness = (pixel.r + pixel.g + pixel.b) / 3f;
                            //Debug.Log(brightness);
                            if (brightness > brightnessThreshold)
                            {
                                pixel.a = 0;
                                texture.SetPixel(i, j, pixel);
                                //Debug.Log("white pixel eliminated");
                            }
                        }
                    }
                    
                    texture.Apply();
                    Rect rect = new Rect(0, 0, texture.width, texture.height);
                    Vector2 pivot = new Vector2(0.5f, 0.5f);

                    Sprite sprite = Sprite.Create(texture, rect, pivot);
                    //var sprite = Sprite.Create(texture, new Rect(0, 0, 256, 256), Vector2.zero, 1f);
                    
                    _image.sprite = sprite;

                    _monsterTypesSO.Sprites.Add(sprite);
                    
                   // AssetDatabase.CreateAsset(sprite, "Assets/AI/GeneratedSprites/SavedSprite.asset");
                    //AssetDatabase.SaveAssets();
                    _monsterTypesSO.ChangeSprites();
                }
            }
            else
            {
                Debug.LogWarning("No image was created from this prompt.");
            }

            _button.enabled = true;
            _inputField.enabled = true;
            _loadingLabel.SetActive(false);
        }
    }
    
}
