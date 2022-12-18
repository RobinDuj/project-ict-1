using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
using System.IO.Ports;
using System.Diagnostics;
using System.Timers;
using static System.Formats.Asn1.AsnWriter;
using System.Threading;
using System.Windows.Documents.Serialization;

namespace project_ict_thomas_is_git
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SerialPort serialPort = new SerialPort();
        bool _i = true;
        bool _spelBezig = false;
        char _letter;
        char _textInput;
        int _tijd = 0;
        int _tijdTimer;
        int _verminderingsGraad;
        Score score = new Score();
        RandomLetters randomLetters = new RandomLetters();
        System.Timers.Timer aTimer = new System.Timers.Timer();

        public MainWindow()
        {
            InitializeComponent();
            cbxPortName.Items.Add("None");
            cbxMoeilijkheid.Items.Add("Easy");
            cbxMoeilijkheid.Items.Add("Hard");
            cbxMoeilijkheid.Items.Add("Gamer");
            foreach (string s in SerialPort.GetPortNames())
            cbxPortName.Items.Add(s);
            _tijdTimer = 400;
            aTimer.Elapsed += new ElapsedEventHandler(Timer_Tick);
            aTimer.Interval = _tijdTimer;
        }

        private void Window_Closed(object sender, EventArgs e) //Wanneer het programma wordt afgesloten
        {
            serialPort.WriteLine("Daaaag");
            var milliseconds = 200;
            Thread.Sleep(milliseconds);
            serialPort.WriteLine("aaaag");
            Thread.Sleep(milliseconds);
            serialPort.WriteLine("aaag");
            Thread.Sleep(milliseconds);
            serialPort.WriteLine("aaag");
            Thread.Sleep(milliseconds);
            serialPort.WriteLine("aag");
            Thread.Sleep(milliseconds);
            serialPort.WriteLine("ag");
            Thread.Sleep(milliseconds);
            serialPort.WriteLine("g");
            Thread.Sleep(milliseconds);
            serialPort.WriteLine("");
            serialPort.Dispose();
        }

        private void cbxPortName_SelectionChanged(object sender, SelectionChangedEventArgs e) //Wanneer er een COM-poort wordt geselecteerd
        {
            if (serialPort != null)
            {
                if (serialPort.IsOpen)
                    serialPort.Close();

                if (cbxPortName.SelectedItem.ToString() != "None")
                {
                    serialPort.PortName = cbxPortName.SelectedItem.ToString();
                    serialPort.Open();
                }
                else
                {
                    MessageBox.Show("Kies een COM-poort", "Fout",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)   //start het spel
        {
            if ((_i) && (serialPort != null) && (serialPort.IsOpen) && (_verminderingsGraad != 0))
            {
                serialPort.WriteLine("Welkom");
                score.Null();
                _letter = randomLetters.GetLetter();
                lblletter.Content = $"Letter: {_letter}";
                serialPort.WriteLine($"{_letter}");
                _i = false;
                _spelBezig = true;
                _tijdTimer = 400;
                aTimer.Enabled = true;
                aTimer.Interval = _tijdTimer;
                lblletter.Content = $"Letter: {_letter}";
                lblScore.Content = "Score:";
            }
            else
            {
                _i = true;
                _spelBezig = false;
                MessageBox.Show("Vul alle velden in!", "Fout",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        private void Timer_Tick(object sender, EventArgs e)     //wanneer de tijd van de timer is gepasseerd
        {
           if(_spelBezig)
            inlezenCijfers();
        }

        private void inlezenCijfers()   //Toont de letter op het display
        {
            if (_textInput == _letter)
            {
                score.Verhogen();
                VolgendLetterAsync();
            }
            else if (_tijd == 1)
            {
                serialPort.WriteLine($" {_letter}");
            }
            else if (_tijd == 2)
            {
                serialPort.WriteLine($"  {_letter}");
            }
            else if (_tijd == 3)
            {
                serialPort.WriteLine($"  {_letter}");
            }
            else if (_tijd == 4)
            {
                serialPort.WriteLine($"   {_letter}");
            }
            else if (_tijd == 5)
            {
                serialPort.WriteLine($"     {_letter}");
            }
            else if (_tijd == 6)
            {
                serialPort.WriteLine($"      {_letter}");
            }
            else if (_tijd == 7)
            {
                serialPort.WriteLine($"       {_letter}");
            }
            else if (_tijd == 8)
            {
                serialPort.WriteLine($"        {_letter}");
            }
            else if (_tijd == 9)
            {
                serialPort.WriteLine($"         {_letter}");
            }
            else if (_tijd == 10)
            {
                serialPort.WriteLine($"          {_letter}");
            }
            else if (_tijd == 11)
            {
                serialPort.WriteLine($"           {_letter}");
            }
            else if (_tijd == 12)
            {
                serialPort.WriteLine($"            {_letter}");
            }
            else if (_tijd == 13)
            {
                serialPort.WriteLine($"             {_letter}");
            }
            else if (_tijd == 14)
            {
                serialPort.WriteLine($"              {_letter}");
            }
            else if (_tijd == 15)
            {
                serialPort.WriteLine($"               {_letter}");
            }
            else if (_tijd > 15)
            {
                gameOverAsync();
            }
            _tijd++;
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)    //Stop het spel
        {
            if ((serialPort != null) && (serialPort.IsOpen) && (_spelBezig = false))
            {
                serialPort.WriteLine("Gestopt");
                _i = true;
                _spelBezig = false;
                aTimer.Enabled = false;
                _tijd = 0;
                lblletter.Content = "Letter:";
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)      //Als er een toest is ingedrukt
        {
            _textInput = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            _textInput = char.ToLower(_textInput);

            if ((_textInput != _letter) && (_tijd >= 0))
            {
                gameOverAsync();
            }
            inlezenCijfers();
        }

        private async Task gameOverAsync()
        {
            serialPort.WriteLine($"Score: {score.Show()}");
            _i = true;
            _spelBezig = false;
            _tijd = 0;
            aTimer.Enabled = false;
            await Task.Run(() => this.Dispatcher.Invoke(() => { lblletter.Content = "Game Over"; }));
            _tijdTimer = 400;
        }

        private async Task VolgendLetterAsync()     //toon het volgende random letter
        {
            if(_tijdTimer <= 20)
            {
                _tijdTimer = 20;
                aTimer.Interval = _tijdTimer;
            }
            else
            {
                _tijdTimer = _tijdTimer - _verminderingsGraad;
                aTimer.Interval = _tijdTimer;
            }
            _tijd = 0;
            _letter = randomLetters.GetLetter();
            await Task.Run(() => this.Dispatcher.Invoke(() => { lblScore.Content = $"Score: {score.Show()}"; }));
            lblletter.Content = $"Letter: {_letter}";
            serialPort.WriteLine($"{_letter}");
        }

        private void cbxMoeilijkheid_SelectionChanged(object sender, SelectionChangedEventArgs e) //moeilijkheidsgraad selecteren
        {
            if(cbxMoeilijkheid.SelectedItem.ToString() == "Easy")
            {
                _verminderingsGraad = 5;
            }
            else if(cbxMoeilijkheid.SelectedItem.ToString() == "Hard")
            {
                _verminderingsGraad = 25;
            }
            else if(cbxMoeilijkheid.SelectedItem.ToString() == "Gamer")
            {
                _verminderingsGraad = 50;
            }
        }
    }
}
