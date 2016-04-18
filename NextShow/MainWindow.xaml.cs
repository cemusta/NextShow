using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;


namespace NextShow
{


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        TimeSpan Count;
        DateTime Start;
        string NextShow;
        string LabelCounting;
        string LabelStarting;

        public delegate void TimerEvent(string sampleParam);

        public event TimerEvent timerEvt;


        public MainWindow()
        {
            int defaultMin = 10;
            NextShow = "Next show";
            LabelCounting = "begins in";
            LabelStarting = "begins";

            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 40);
            dispatcherTimer.Start();
            Count = new TimeSpan(0, defaultMin, 1);
            Start = DateTime.Now;

            Mouse.OverrideCursor = Cursors.None;

            this.KeyDown += new KeyEventHandler(OnButtonKeyDown);

            InitializeComponent();
        }

        private void OnButtonKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                Start += new TimeSpan(0, 1, 0);
            }
            else if (e.Key == Key.Down)
            {
                Start -= new TimeSpan(0, 1, 0);
            }
            else if (e.Key == Key.Left)
            {
                Start += new TimeSpan(0, 0, 1);
            }
            else if (e.Key == Key.Right)
            {
                Start -= new TimeSpan(0, 0, 1);
            }
            else if (e.Key == Key.Enter)
            {
                Start = DateTime.Now + new TimeSpan(0, 10, 0);
            }
            else if (e.Key == Key.D1)
            {
                NextShow = "Next show";
            }
            else if (e.Key == Key.D2)
            {
                NextShow = "Tracked show";
            }
            else if (e.Key == Key.D3)
            {
                NextShow = "Trackless show";
            }
            else if(e.Key == Key.C && Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                Application.Current.Shutdown();
            }
            else if (e.Key == Key.F1)
            {
                NextShow = "Tracked show";
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            timer.Foreground = Brushes.Red;

            // code goes here
            TimeSpan diff = Start - DateTime.Now;
            TimeSpan display = (Count + diff);
            if(display > new TimeSpan(0,1,0))
            {
                timer.Foreground = Brushes.Black;
                nextshow.Text = NextShow;
                begins.Text = LabelCounting;
                timer.Text = $"{display.Hours:D2}:{display.Minutes:D2}:{display.Seconds:D2}";
            }
            else if (display <= new TimeSpan(0, 0, 1))
            {
                nextshow.Text = NextShow;
                begins.Text = LabelStarting;
                timer.Text = "Now";
                Start = DateTime.Now - new TimeSpan(0, 10, 0);
            }
            else if (display <= new TimeSpan(0, 1, 0))
            {
                timer.Foreground = Brushes.Red;
                nextshow.Text = NextShow;
                begins.Text = LabelCounting;
                timer.Text = $"{display.Hours:D2}:{display.Minutes:D2}:{display.Seconds:D2}";
            }
            if (timerEvt != null)
            {
                timerEvt("sample parameter");
            }

        }




    }

    public class MyTextBlock : System.Windows.Controls.TextBlock
    {
        // Create a custom routed event by first registering a RoutedEventID
        // This event uses the bubbling routing strategy
        public static readonly RoutedEvent TapEvent = EventManager.RegisterRoutedEvent(
            "Tap", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(MyTextBlock));

        // Provide CLR accessors for the event
        public event RoutedEventHandler Tap
        {
            add { AddHandler(TapEvent, value); }
            remove { RemoveHandler(TapEvent, value); }
        }

        // This method raises the Tap event
        void RaiseTapEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(MyTextBlock.TapEvent);
            RaiseEvent(newEventArgs);
        }


    }


}
