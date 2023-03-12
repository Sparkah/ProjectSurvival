using UniRx;
using UnityEngine;

namespace _ProjectSurvival.Infrastructure
{
    public class World
    {
        public ReactiveProperty<float> Gold { get; set; } = new ReactiveProperty<float>(0);

        public World()
        {
        }
    }
}