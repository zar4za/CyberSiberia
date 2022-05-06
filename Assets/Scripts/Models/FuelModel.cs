using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class FuelModel
    {
        private const int MaxError = 10;

        public Color Color { get; } 
        public int Gasoline { get; }
        public int Hydrogen { get; }
        public int Kerosene { get; }
        public int Alcohol { get; }

        public FuelModel(Color color)
        {
            Color = color;
        }

        public FuelModel(Color color, Dictionary<Liquid.LiquidType, float> liquidVolumes)
        {
            float volume = liquidVolumes.Sum(x => x.Value);
            Color = color;
            Gasoline = Mathf.RoundToInt((liquidVolumes[Liquid.LiquidType.Gasoline] / volume) * 100);
            Hydrogen = Mathf.RoundToInt((liquidVolumes[Liquid.LiquidType.Hydrogen] / volume) * 100);
            Kerosene = Mathf.RoundToInt((liquidVolumes[Liquid.LiquidType.Kerosene] / volume) * 100);
            Alcohol = Mathf.RoundToInt((liquidVolumes[Liquid.LiquidType.Alcohol] / volume) * 100);
        }

        public FuelModel(Color color, int gasoline, int hydrogen, int kerosene, int alcohol)
        {
            Color = color;
            Gasoline = gasoline;
            Hydrogen = hydrogen;
            Kerosene = kerosene;
            Alcohol = alcohol;
        }

        public static bool AreSimilar(FuelModel requested, FuelModel produced)
        {
            var gasolineOk = Mathf.Abs(requested.Gasoline - produced.Gasoline) <= MaxError;
            var hydrogenOk = Mathf.Abs(requested.Hydrogen - produced.Hydrogen) <= MaxError;
            var keroseneOk = Mathf.Abs(requested.Kerosene - produced.Kerosene) <= MaxError;
            var alcoholOk = Mathf.Abs(requested.Alcohol - produced.Alcohol) <= MaxError;

            return gasolineOk && hydrogenOk && keroseneOk && alcoholOk;
        }

        public static FuelModel CreateRandom()
        {
            int min = 1;
            int max = 99;
            int total = min + max;

            int gasoline = Random.Range(min, max);
            int hydrogen = Random.Range(min, total - gasoline);
            int kerosene = Random.Range(min, total - gasoline - hydrogen);
            int alcohol = total - gasoline - hydrogen - kerosene;

            Debug.Log($"Created: g-{gasoline} h-{hydrogen} k-{kerosene} a-{alcohol}");
            return new FuelModel(Color.black, gasoline, hydrogen, kerosene, alcohol);
        }
    }
}
