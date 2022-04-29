using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
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
using System.Windows.Threading;

namespace WpfControlLibrary
{

    public partial class Clock : UserControl
    {
        public Clock()
        {
            InitializeComponent();
            foreach (var i in TimeZones)
                slc.Items.Add(i);    
            slc.SelectedIndex = 0;
            StartClock();
            HourMarks();
            MinuteMarks();
        }

        public List<TimeZoneInfo> TimeZones
        {
            get 
            { 
                return TimeZoneInfo.GetSystemTimeZones().ToList(); 
            }
        }

        const double NumberSystem = 60;
        const double baseAngleNumberSystem = 360 / NumberSystem;    // Базовый угол для минут и секунд.
        const double baseAngleHour = 30;    // Базовый угол для часов.

        void StartClock()
        {
            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {

            int sec = DateTime.Now.Second;
            int min = DateTime.UtcNow.AddMinutes(((TimeZoneInfo)slc.SelectedItem).BaseUtcOffset.TotalMinutes).Minute;
            int hour = DateTime.UtcNow.AddHours(((TimeZoneInfo)slc.SelectedItem).BaseUtcOffset.TotalHours).Hour;

            var rotateSecondArrow = new RotateTransform();
            var rotateMinuteArrow = new RotateTransform();
            var rotateHourArrow = new RotateTransform();

            rotateSecondArrow.Angle = baseAngleNumberSystem * sec;  // Вычисленный угол для секундной стрелки.
            SecondArrow.RenderTransform = rotateSecondArrow;    //поворот 

            rotateMinuteArrow.Angle = (min * baseAngleNumberSystem) + (rotateSecondArrow.Angle / 60.0); // Вычисленный угол для минутной стрелки.
            MinuteArrow.RenderTransform = rotateMinuteArrow;

            rotateHourArrow.Angle = (hour - 12) * baseAngleHour + rotateMinuteArrow.Angle / 12; // Вычисленный угол для часовой стрелки.
            HourArrow.RenderTransform = rotateHourArrow;
        }

        void MinuteMarks()
        {
            for (int i = 0; i < 60; i++)
            {
                var b = new Border()
                {
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Stretch,
                    RenderTransformOrigin = new Point(0.5, 0.5),
                    Height = this.Height / 300
                };

                var b1 = new Border()
                {
                    Background = Brushes.Red,
                    Width = this.Width/60,
                    HorizontalAlignment = HorizontalAlignment.Right
                };

                b.Child = b1;

                var rotate = new RotateTransform(i * 6);
                b.RenderTransform = rotate;

                if (i * 6 % 30 != 0)
                {
                    ClockFace.Children.Add(b);
                }
            }
        }   // Минутная маркировка циферблата

        void HourMarks()
        {
            for (int i = 0; i < 12; i++)
            {
                var b = new Border()
                {
                    Height = this.Height/53,

                    RenderTransformOrigin = new Point(0.5, 0.5),
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Stretch
                };

                var b1 = new Border()
                {
                    Width = this.Width/75,

                    Background = Brushes.Black,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    BorderBrush = Brushes.Black
                };

                b.Child = b1;

                var rotate = new RotateTransform(i * 30);
                b.RenderTransform = rotate;

                ClockFace.Children.Add(b);
            }
        }   // Часовая маркировка циферблата
    }
}
