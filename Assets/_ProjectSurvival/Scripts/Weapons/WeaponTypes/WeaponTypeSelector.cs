using UnityEngine;
using Zenject;

public class WeaponTypeSelector : MonoBehaviour
{
    [Inject] private ActiveWeapons _activeWeapons;
    [SerializeField] private AvailableWeaponsSO _availableWeaponsSO;

    public WeaponTypeSO[] SelectWeapons(int count)
    {
        WeaponTypeSO[] availableWeapons = _availableWeaponsSO.SelectAvailableWeapons(_activeWeapons);

        RandomRangeSelector randomRangeSelector = new RandomRangeSelector();
        int[] selectedIndexes = randomRangeSelector
            .SelectRandomIndexes(count, availableWeapons.Length);

        WeaponTypeSO[] selectedWeapons = new WeaponTypeSO[selectedIndexes.Length];
        for (int i = 0; i < selectedIndexes.Length; i++)
            selectedWeapons[i] = availableWeapons[selectedIndexes[i]];

        return selectedWeapons;
    }
}
