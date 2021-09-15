using System;
using System.Collections.Generic;
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

namespace ChatWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Windows.Threading.DispatcherTimer Timer = new System.Windows.Threading.DispatcherTimer();
        List<string> randomMessages = new List<string>()
        {
            "Aleykum salam",
            "Saol sen necesen",
            "Sabah gelecem danisarig",
            "Ok",
            "Sagol",
            "Oldu"
        };
        public MainWindow()
        {
            InitializeComponent();
            Timer.Tick += new EventHandler(Timer_Click);
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Start();
        }

        private void Timer_Click(object sender, EventArgs e)
        {
            DateTime d;
            d = DateTime.Now;
            if (d.Minute < 10)
                lblWatch.Content = d.Hour + ":0" + d.Minute;
            else
                lblWatch.Content = d.Hour + ":" + d.Minute;

        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Image_Close_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb)
            {
                if (tb.Text == "Write a message...")
                {
                    tbMes.IsEnabled = true;
                    tb.Text = "";
                    tb.Foreground = Brushes.White;
                    tb.FontWeight = FontWeights.Normal;
                }
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb)
            {
                if (string.IsNullOrWhiteSpace(tb.Text))
                {
                    tbMes.IsEnabled = false;
                    tb.Text = "Write a message...";
                    tb.Foreground = Brushes.Silver;
                    tb.FontWeight = FontWeights.Bold;
                }
            }
        }

        private StackPanel BotMessage()
        {
            Random random = new Random();
            var stackPanel = new StackPanel();

            var border = new Border()
            {
                CornerRadius = new CornerRadius(0, 15, 15, 15),
                Background = Brushes.White
            };

            var grid = new Grid();
            var textBlock = new TextBlock()
            {
                TextWrapping = TextWrapping.Wrap,
                Name = "textBlockUser",
                MaxWidth = 200,
                Foreground = Brushes.Black,
                Margin = new Thickness(10),
                Text = randomMessages[random.Next(0, randomMessages.Count)]
            };
            string minute = "0";
            if (DateTime.Now.Minute < 10)
            {
                minute = "0" + DateTime.Now.Minute;
            }
            else
            {
                minute = DateTime.Now.Minute.ToString();
            }
            var label = new Label()
            {
                Foreground = Brushes.Black,
                FontSize = 12,
                FontWeight = FontWeights.SemiBold,
                HorizontalAlignment = HorizontalAlignment.Right,
                Content = DateTime.Now.Hour + ":" + minute
            };
            grid.Children.Add(textBlock);
            border.Child = grid;
            stackPanel.Children.Add(border);
            stackPanel.Children.Add(label);
            return stackPanel;
        }

        private void Button_Sent_Click(object sender, RoutedEventArgs e)
        {
            tbMes.IsEnabled = true;
            var stackPanel = new StackPanel()
            {
                Margin = new Thickness(0, 10, 5, 0)
            };

            var border = new Border()
            {
                CornerRadius = new CornerRadius(15, 0, 15, 15),
                HorizontalAlignment = HorizontalAlignment.Left,
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#859FFE"))
            };

            var textBlock = new TextBlock()
            {

                HorizontalAlignment = HorizontalAlignment.Left,
                TextWrapping = TextWrapping.Wrap,
                Name = "textBlockUser",
                MaxWidth = 200,
                Background = Brushes.Transparent,
                Foreground = Brushes.White,
                Margin = new Thickness(10),
                Text = tbMes.Text
            };
            string minute = "0";
            if (DateTime.Now.Minute < 10)
            {
                minute = "0" + DateTime.Now.Minute;
            }
            else
            {
                minute = DateTime.Now.Minute.ToString();
            }
            var label = new Label()
            {
                Foreground = Brushes.Black,
                FontSize = 12,
                FontWeight = FontWeights.SemiBold,
                HorizontalAlignment = HorizontalAlignment.Right,
                Content = DateTime.Now.Hour + ":" + minute
            };

            border.Child = textBlock;
            stackPanel.Children.Add(border);
            stackPanel.Children.Add(label);

            listBox.Items.Add(stackPanel);
            Thread.Sleep(700);
            listBox.Items.Add(BotMessage());
            tbMes.Text = "Write a message...";
            tbMes.Foreground = Brushes.Silver;
            tbMes.FontWeight = FontWeights.Bold;
        }
    }
}
