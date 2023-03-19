using _ProjectSurvival.Infrastructure;
using UniRx;
using UnityEngine;
using Zenject;

namespace _ProjectSurvival.Scripts.Gold
{
    public class GoldAmountUI : MonoBehaviour
    {
        [Inject] private World _world;
        [SerializeField] private TMPro.TMP_Text _amountLabel;
        private CompositeDisposable _disposables = new CompositeDisposable();

        private void Start()
        {
            PrintFormattedCoins(_world.Gold.Value);
            _world.Gold.Subscribe(x => PrintFormattedCoins(x)).AddTo(_disposables);
        }

        private void OnDestroy()
        {
            _disposables.Clear();
        }

        private void PrintFormattedCoins(float coinsAmount)
        {
            _amountLabel.text = Mathf.FloorToInt(coinsAmount).ToString();
        }
    }
}
