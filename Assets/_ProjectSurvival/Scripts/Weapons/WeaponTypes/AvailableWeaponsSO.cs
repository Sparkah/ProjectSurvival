using System.Collections.Generic;
using UnityEngine;

namespace _ProjectSurvival.Scripts.Weapons.WeaponTypes
{
    [CreateAssetMenu(fileName = "AvailableWeapons", menuName = "Survivors prototype/Available weapons", order = 1)]
    public class AvailableWeaponsSO : ScriptableObject
    {
        [SerializeField] private WeaponTypeSO[] _weaponTypeSOs;

        public WeaponTypeSO[] SelectAvailableWeapons(ActiveWeapons.ActiveWeapons activeWeapons)
        {
            List<WeaponTypeSO> availableWeapons = new List<WeaponTypeSO>();
            for (int i = 0; i < _weaponTypeSOs.Length; i++)
            {
                if (!activeWeapons.HasMaximumLevel(_weaponTypeSOs[i]))
                    availableWeapons.Add(_weaponTypeSOs[i]);
            }
            return availableWeapons.ToArray();
        }
    }
}
