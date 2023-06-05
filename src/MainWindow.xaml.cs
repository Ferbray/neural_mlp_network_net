using src.Common;
using src.Services;
using System;
using System.Collections.Generic;
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

namespace src
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double District { get; set; }
        private double House { get; set; }
        private double Status { get; set; }
        private double NumbersFloors { get; set; }
        private double Floor { get; set; }
        private double Square { get; set; }
        private double NumberRooms { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            district.Items.Add("Центр");
            district.Items.Add("Отдаленные районы");
            house.Items.Add("Кирпичный");
            house.Items.Add("Панельный");
            house.Items.Add("Шлакоблочный");
            status.Items.Add("Евроремонт");
            status.Items.Add("Требует ремонта");
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                District = WeightData.Datas
                    .First(kvp => kvp.Key == district.Text.ToLower()).Value;
                House = WeightData.Datas
                    .First(kvp => kvp.Key == house.Text.ToLower()).Value;
                Status = WeightData.Datas
                    .First(kvp => kvp.Key == status.Text.ToLower()).Value;
                NumbersFloors = Convert.ToDouble(numbersFloors.Text);
                Floor = Convert.ToDouble(floor.Text);
                Square = Convert.ToDouble(square.Text);
                NumberRooms = Convert.ToDouble(numbersRooms.Text);

                double[] input = new double[] { 
                    District, House, Status, 
                    NumbersFloors / 10, Floor / 10, Square / 100, 
                    NumberRooms / 10
                };

                double res = MLPService.Learning(input);

                if (double.IsNaN(res) || res == 0)
                    error.Text = "Нейронка не обучилась";

                result.Text = res.ToString();
            }
            catch(Exception ex)
            {
                error.Text = $"{ex.Message}";
            }
        }
    }
}
