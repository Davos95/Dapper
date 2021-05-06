using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleDapper.Model
{
    public class Car
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Enrollment { get; set; }


        public List<Car> GetCarsDefault()
        {
            var car1 = new Car() { Brand = "Hyundai", Model = "i20", Color = "Intense Blue" };
            var car2 = new Car() { Brand = "Opel", Model = "Corsa", Color = "Red" };
            var car3 = new Car() { Brand = "Seat", Model = "Panda", Color = "White" };

            var cars = new List<Car>();
            cars.Add(car1);
            cars.Add(car2);
            cars.Add(car3);

            return cars;
        }
    }
}
