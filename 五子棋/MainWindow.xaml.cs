using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace 五子棋
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        List<Button> qis = new List<Button>();int turnto = 0;//0:me;1:that
        List<Button> cqis = new List<Button>();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Width = Width * (Width / g1.ActualWidth);
            Height = Height * (Height / g1.ActualHeight);

            for (int i = 0;i < 50;i++)
            {
                for (int i2 = 0; i2 < 50; i2++)
                {
                    Button b = new Button();
                    b.HorizontalAlignment = HorizontalAlignment.Left;
                    b.VerticalAlignment = VerticalAlignment.Top;
                    b.Margin = new Thickness(i * 10, i2 * 10, 0, 0);
                    b.Width = b.Height = 10;
                    b.Background = Brushes.White;
                    b.Click += (a, ee) =>
                    {
                        if (b.Background != Brushes.White)
                            return;
                        cqis.Add(b);
                        if (turnto == 0)
                        {
                            Title = "轮到蓝色";
                            turnto = 1;
                            b.Background = Brushes.Red;
                        }
                        else
                        {
                            Title = "轮到红色";
                            turnto = 0;
                            b.Background = Brushes.Blue;
                        }
                        Thickness mycenter = new Thickness(b.Margin.Left + 5, b.Margin.Top + 5, 0, 0);
                        foreach(Button bb in qis)
                        {
                            //左斜
                            int tleft = 0;
                            for(int ni = -4; ni < 5;ni ++)//0=me
                            {
                                if (findbuttonbypos(new Thickness(mycenter.Left + ni * 10,mycenter.Top + ni * 10,0,0)).Background == b.Background)
                                {
                                    tleft++;
                                    if (tleft == 5)
                                        break;
                                }
                                else
                                {
                                    tleft = 0;
                                }
                            }
                            //右斜
                            int tright = 0;
                            for (int ni = -4; ni < 5; ni++)//0=me
                            {
                                if (findbuttonbypos(new Thickness(mycenter.Left + ni * 10, mycenter.Top - ni * 10, 0, 0)).Background == b.Background)
                                {
                                    tright++;
                                    if (tright == 5)
                                        break;
                                }
                                else
                                {
                                    tright = 0;
                                }
                            }
                            //向左右
                            int lr = 0;
                            for (int ni = -4; ni < 5; ni++)//0=me
                            {
                                if (findbuttonbypos(new Thickness(mycenter.Left + ni * 10, mycenter.Top, 0, 0)).Background == b.Background)
                                {
                                    lr++;
                                    if (lr == 5)
                                        break;
                                }
                                else
                                {
                                    lr = 0;
                                }
                            }
                            //向上下
                            int tb = 0;
                            for (int ni = -4; ni < 5; ni++)//0=me
                            {
                                if (findbuttonbypos(new Thickness(mycenter.Left, mycenter.Top + ni * 10, 0, 0)).Background == b.Background)
                                {
                                    tb++;
                                    if (tb == 5)
                                        break;
                                }
                                else
                                {
                                    tb = 0;
                                }
                            }
                            if (tleft > 4 || tright > 4 || lr > 4 || tb > 4)
                            {
                                if (b.Background == Brushes.Red)
                                    MessageBox.Show("红色赢了");
                                else
                                    MessageBox.Show("蓝色赢了");
                                Hide();
                                MainWindow m = new MainWindow();
                                m.ShowDialog();
                                return;
                            }
                        }
                    };
                    qis.Add(b);
                    g1.Children.Add(b);
                }
            }
        }

        public Button findbuttonbypos(Thickness th)
        {
            Rect r1 = new Rect(th.Left, th.Top, 1, 1);
            foreach(Button b in cqis)
            {
                Rect r2 = new Rect(b.Margin.Left, b.Margin.Top, 10, 10);
                if (r2.IntersectsWith(r1))
                {
                    return b;
                }
            }
            return new Button();
        }
    }
}
