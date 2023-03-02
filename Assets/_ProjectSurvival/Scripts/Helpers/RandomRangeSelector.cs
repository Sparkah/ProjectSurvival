using System.Collections.Generic;
using UnityEngine;

public class RandomRangeSelector
{
    public int[] SelectRandomIndexes(int amount, int rangeSize)
    {
        List<int> selectedIndexes = new List<int>();

        if (amount >= rangeSize)
        {
            AddRangeToList(selectedIndexes, 0, rangeSize);
        }
        else
        {
            int indexPerPlace = rangeSize / amount;
            if (indexPerPlace < 2)
            {
                AddRangeToList(selectedIndexes, 0, amount);
            }
            else
            {
                int initStep = 0;
                int targetStep = indexPerPlace;
                int selectedIndex;
                for (int i = 0; i < amount; i++)
                {
                    selectedIndex = Random.Range(initStep, targetStep);
                    selectedIndexes.Add(selectedIndex);
                    initStep += indexPerPlace;
                    targetStep += indexPerPlace;
                }
            }
        }
        return selectedIndexes.ToArray();
    }

    private void AddRangeToList(List<int> list, int from, int count)
    {
        for (int i = from; i < count; i++)
        {
            list.Add(i);
        }
    }
}
