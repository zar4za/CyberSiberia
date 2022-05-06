using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class LiquidModel
    {
        public Color Color { get; }
        public Liquid.LiquidType Liquid { get; }
        public float Amount { get; private set; }

        public LiquidModel(Color color, Liquid.LiquidType liquid, float amount)
        {
            Color = color;
            Liquid = liquid;
            Amount = amount;
        }

        public void AddLiquid(float amount)
        {
            Amount += amount;
        }
    }
}
