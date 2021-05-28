using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Projekt_Systemy
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
           
        }
        Random rand = new Random();
        const int minTrainTime = 5000;
        const int maxTrainTime = 10000;
        bool lightSignal = false;

        private Point betweenPoint(Point one, Point two, int procent, int maxProcent)
        {
            return new Point(one.X+(((two.X-one.X)*procent)/ maxProcent), one.Y + (((two.Y - one.Y) * procent)/ maxProcent));
        }

        private void StartCar()
        {
            Point xy = new Point(-50, 110);
            Point bezieraHorizontal = new Point(510, 110);
            Point bezieraVertical = new Point(610, 110);
            int procentTurn = 0;


            while (xy.X > 510)
            {
                this.Dispatcher.Invoke((Action)(() =>
                {
                    MoveCar(xy, car1);
                }));
                System.Threading.Thread.Sleep(5);
                xy.X++;
            }
            while (bezieraVertical.Y > 200)
            {
                xy = betweenPoint(bezieraHorizontal, bezieraVertical, procentTurn, 90);
                this.Dispatcher.Invoke((Action)(() =>
                {
                    MoveCar(xy, car1);
                }));
                bezieraHorizontal.X++;
                bezieraVertical.Y++;
                procentTurn++;
                System.Threading.Thread.Sleep(10);
            }
            bezieraHorizontal.Y = 290;
            while (bezieraVertical.Y > 290)
            {
                xy = betweenPoint(bezieraHorizontal, bezieraVertical, procentTurn, 90);
                this.Dispatcher.Invoke((Action)(() =>
                {
                    MoveCar(xy, car1);
                }));
                bezieraHorizontal.X--;
                bezieraVertical.Y++;
                procentTurn--;
                System.Threading.Thread.Sleep(10);
            }
            while (xy.X < 170)
            {
                this.Dispatcher.Invoke((Action)(() =>
                {
                    MoveCar(xy, car1);
                }));
                System.Threading.Thread.Sleep(5);
                xy.X--;
            }
            bezieraHorizontal.X = 170;
            bezieraVertical.X = 100;
            while (bezieraVertical.Y > 350)
            {
                xy = betweenPoint(bezieraHorizontal, bezieraVertical, procentTurn, 60);
                this.Dispatcher.Invoke((Action)(() =>
                {
                    MoveCar(xy, car1);
                }));
                bezieraHorizontal.X--;
                bezieraVertical.Y++;
                procentTurn++;
                System.Threading.Thread.Sleep(10);
            }
            bezieraHorizontal.Y = 410;
            while (bezieraVertical.Y > 410)
            {
                xy = betweenPoint(bezieraHorizontal, bezieraVertical, procentTurn, 60);
                this.Dispatcher.Invoke((Action)(() =>
                {
                    MoveCar(xy, car1);
                }));
                bezieraHorizontal.X++;
                bezieraVertical.Y++;
                procentTurn--;
                System.Threading.Thread.Sleep(10);
            }
            while (xy.X > 800)
            {
                while (lightSignal && xy.X>590 && xy.X<610)
                {
                    System.Threading.Thread.Sleep(50);
                }
                this.Dispatcher.Invoke((Action)(() =>
                {
                    MoveCar(xy, car1);
                }));
                System.Threading.Thread.Sleep(5);
                xy.X++;
            }
        }

        void StartTrain()
        {
            while (true)
            {
                Thread.Sleep(rand.Next(minTrainTime, maxTrainTime));
                lightSignal = true;
                int x = -370;
                while (true)
                {
                    
                    this.Dispatcher.Invoke((Action)(() =>
                    {
                        Canvas.SetTop(pociag, x);
                    }));
                    System.Threading.Thread.Sleep(5);
                    x++;
                    if (x > 600)
                    {
                        break;
                    }
                }
                lightSignal = false;
            }
        }

        void StartLights()
        {
            bool rightLight = true;
            while (true)
            {
                Thread.Sleep(300);
                this.Dispatcher.Invoke((Action)(() =>
                    {
                        if (lightSignal)
                        {
                            if (rightLight)
                            {
                                light.Source = new BitmapImage(new Uri("/src/lewy.png", UriKind.Relative));
                                rightLight = !rightLight;
                            }
                            else
                            {
                                light.Source = new BitmapImage(new Uri("/src/prawy.png", UriKind.Relative));
                                rightLight = !rightLight;
                            }
                        }
                        else
                        {
                            light.Source = new BitmapImage(new Uri("/src/brak.png", UriKind.Relative));
                        }
                    }));
            }
        }

        private void buttonStart(object sender, RoutedEventArgs e)
        {
            Thread tThread = new Thread(new ThreadStart(StartTrain));
            tThread.IsBackground = true;
            tThread.Start();
            Thread cThread = new Thread(new ThreadStart(StartCar));
            cThread.IsBackground = true;
            cThread.Start();
            Thread lThread = new Thread(new ThreadStart(StartLights));
            lThread.IsBackground = true;
            lThread.Start();
        }

        private void MoveCar(Point xy, Image car)
        {
            Canvas.SetLeft(car, xy.X);
            Canvas.SetTop(car, xy.Y);
        }
    }
}
