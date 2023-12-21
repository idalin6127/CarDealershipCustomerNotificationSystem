using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegate
{

    public class Car
    {
        public string Model { get; set; }
        public string Color { get; set; }
        public int Year { get; set; }
        public int Mileage { get; set; }
        public decimal Price { get; set; }

        public Car(string model, string color, int year, int mileage, decimal price)
        {
            Model = model;
            Color = color;
            Year = year;
            Mileage = mileage;
            Price = price;
        }
    }
}
