using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

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
        public int skret = 1;// w ktorym kierunku ma byc obrocony pojazd i z ktorego kierunku jest kolizja
        public int procentTurn = 0;
        public BitmapImage bitR = new BitmapImage();
        public BitmapImage bitL = new BitmapImage();

        public Car()
        {
        }

        public Car(string src)
        {
            bitR.BeginInit();
            bitR.UriSource = new Uri(src + "_r.png", UriKind.Relative);
            bitR.EndInit();

            bitL.BeginInit();
            bitL.UriSource = new Uri(src + "_l.png", UriKind.Relative);
            bitL.EndInit();
        }

        public void getNewValues(Random rand)
        {
            coordinates = new Point(-50, 110);
            skret = 1;
            procentTurn = 0;
            speed = rand.Next(maxCarSpeed, minCarSpeed);
            // ustawienie nowych parametrów dla pojazdu
        }
    }
}
