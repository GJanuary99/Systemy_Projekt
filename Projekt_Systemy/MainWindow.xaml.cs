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
        public bool lightSignal = false;

        private Point betweenPoint(Point one, Point two, int procent, int maxProcent)
        {
            return new Point(one.X+(((two.X-one.X)*procent)/ maxProcent), one.Y + (((two.Y - one.Y) * procent)/ maxProcent));
            // zwraca punkt miedzy dwoma punktami na danej odleglosci od punktu pierwszego
        }

        private void StartCar(Car car, Image image)
        {
            while (true)
            {
                car.getNewValues(rand);
                Dispatcher.Invoke((Action)(() =>
                {
                    Canvas.SetLeft(image, car.coordinates.X);
                    Canvas.SetTop(image, car.coordinates.Y);
                    // ustawienie pojazdu na opdowiednich koordynatach, resetowanie go na poczatek
                }));
                Thread.Sleep(rand.Next(minTrainTime / 4, maxTrainTime / 2)); // czas oczekiwania zanim pojedzie auto
                while (car.coordinates.X < 520)
                {
                    MoveCar(car.coordinates, image, car.skret);
                    System.Threading.Thread.Sleep(car.speed);
                    car.coordinates.X++;
                    // pierwszy odcinek
                }
                car.bezieraHorizontal = new Point(car.coordinates.X, car.coordinates.Y);
                car.bezieraVertical = new Point(car.coordinates.X + 90, car.coordinates.Y);
                while (car.bezieraVertical.Y < 290)
                {
                    car.coordinates = betweenPoint(car.bezieraHorizontal, car.bezieraVertical, car.procentTurn, 90);
                    MoveCar(car.coordinates, image, car.skret);
                    car.bezieraHorizontal.X += car.skret;
                    car.bezieraVertical.Y++;
                    car.procentTurn += car.skret;
                    if (car.bezieraVertical.Y == 200)
                    {
                        Dispatcher.Invoke((Action)(() =>
                        {
                            image.Source = car.bitL;//obrot samochodu
                        }));
                        car.skret = car.skret * -1;
                        car.bezieraHorizontal.Y = 290;
                    }
                    System.Threading.Thread.Sleep(car.speed * 2);
                    // pierwszy zakret
                }
                while (car.coordinates.X > 170)
                {
                    MoveCar(car.coordinates, image, car.skret);
                    System.Threading.Thread.Sleep(car.speed);
                    car.coordinates.X--;
                    // drugi odcinek
                }
                car.bezieraHorizontal.X = 170;
                car.bezieraVertical.X = 100;
                while (car.bezieraVertical.Y < 410)
                {
                    car.coordinates = betweenPoint(car.bezieraHorizontal, car.bezieraVertical, car.procentTurn, 60);
                    MoveCar(car.coordinates, image, car.skret);
                    car.bezieraHorizontal.X += car.skret;
                    car.bezieraVertical.Y++;
                    car.procentTurn += car.skret * -1;
                    if (car.bezieraVertical.Y == 350)
                    {
                        Dispatcher.Invoke((Action)(() =>
                        {
                            image.Source = car.bitR;//obrot samochodu
                        }));
                        car.skret = car.skret * -1;
                        car.bezieraHorizontal.Y = 410;
                    }
                    System.Threading.Thread.Sleep(car.speed * 2);
                    // drugi zakret
                }
                while (car.coordinates.X < 800)
                {
                    while (lightSignal && car.coordinates.X > 590 && car.coordinates.X < 610)
                    {
                        System.Threading.Thread.Sleep(50);//zatrzymanie pojazdu gdy jedzie pociag
                    }
                    MoveCar(car.coordinates, image, car.skret);
                    System.Threading.Thread.Sleep(car.speed);
                    car.coordinates.X++;
                    //trzeci odcinek/ostatni przed torami
                }
            }
        }

        void StartTrain()
        {
            while (true)
            {
                Thread.Sleep(rand.Next(minTrainTime, maxTrainTime));
                lightSignal = true;//jezeli pociag jedzie, swiatla dzialaja
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
                lightSignal = false;//wylaczenie swiatel
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
                        if (lightSignal)// animacja mrugania swiatel
                        {
                            rogatka.Height = 150;
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
                            rogatka.Height = 50;
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

            Thread lThread = new Thread(new ThreadStart(StartLights));
            lThread.IsBackground = true;
            lThread.Start();

            Car car1 = new Car("/src/car_blue");
            Thread c1Thread = new Thread(()=> StartCar(car1, carImg1));
            c1Thread.IsBackground = true;
            c1Thread.Start();

            Car car2 = new Car("/src/car_red");
            Thread c2Thread = new Thread(() => StartCar(car2, carImg2));
            c2Thread.IsBackground = true;
            c2Thread.Start();

            Car car3 = new Car("/src/car_black");
            Thread c3Thread = new Thread(() => StartCar(car3, carImg3));
            c3Thread.IsBackground = true;
            c3Thread.Start();

            Car car4 = new Car("/src/car_gray");
            Thread c4Thread = new Thread(() => StartCar(car4, carImg4));
            c4Thread.IsBackground = true;
            c4Thread.Start();

            Car car5 = new Car("/src/car_green");
            Thread c5Thread = new Thread(() => StartCar(car5, carImg5));
            c5Thread.IsBackground = true;
            c5Thread.Start();

            Car car6 = new Car("/src/car_yellow");
            Thread c6Thread = new Thread(() => StartCar(car6, carImg6));
            c6Thread.IsBackground = true;
            c6Thread.Start();
        }

        private void MoveCar(Point xy, Image car, int collisionright)
        {
            bool collision = true;
            while (collision)
            {
                Dispatcher.Invoke((Action)(() =>
                {
                    foreach (FrameworkElement sibling in canvas.Children)// porownujemy odleglosci miedzy naszym pojazdem a wszystkimi z listy canvas 
                    {
                        if (sibling.Name != car.Name)
                        {
                            double distance = Point.Subtract(new Point(xy.X + 25, xy.Y + 25), new Point(Convert.ToInt32(sibling.GetValue(Canvas.LeftProperty))+25, Convert.ToInt32(sibling.GetValue(Canvas.TopProperty))+25)).Length;
                            // dystans pomiedzy autami
                            if (Convert.ToInt32(distance)<75 &&
                            Convert.ToInt32(sibling.GetValue(Canvas.TopProperty))>=xy.Y &&
                            ((Convert.ToInt32(sibling.GetValue(Canvas.LeftProperty))>(xy.X)&&collisionright==1) || (Convert.ToInt32(sibling.GetValue(Canvas.LeftProperty)) < (xy.X + 50) && collisionright == -1))
                            )//sprawdzenie czy auto ma wystarczajacy dystans, jezeli nie to wchodzimy do if'a
                            {
                                collision = true;// wykrycie kolizji i zatrzymanie pojazdu
                                break;
                            }
                        }
                        collision = false;
                    }
                }));
                Thread.Sleep(1);
            }
            Dispatcher.Invoke((Action)(() =>
            {
                Canvas.SetLeft(car, xy.X);
                Canvas.SetTop(car, xy.Y);
                // poruszanie sie pojazdu
            }));
        }
    }
}
