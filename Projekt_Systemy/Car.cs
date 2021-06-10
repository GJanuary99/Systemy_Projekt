using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Projekt_Systemy
{
    public class Car
    {
        const int maxCarSpeed = 2;
        const int minCarSpeed = 7;
        public int speed;
        public Point coordinates;
        public Point bezieraHorizontal;
        public Point bezieraVertical;
        public int skret = 1;
        public int procentTurn = 0;

        public Car()
        {
        }

        public void getNewValues(Random rand)
        {
            coordinates = new Point(-50, 110);
            skret = 1;
            procentTurn = 0;
            speed = rand.Next(maxCarSpeed, minCarSpeed);
        }
    }
}
