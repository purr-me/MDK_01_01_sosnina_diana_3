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
using MyApp3.Classes;
using System.Windows.Threading;

namespace MyApp3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Classes.PersonInfo Player = new Classes.PersonInfo("Student", 100, 10, 1, 0, 0, 5);
        public List<PersonInfo> Enemys = new List<PersonInfo>();
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public Classes.PersonInfo Enemy;

        Random rnd = new Random();


        public MainWindow()
        {
            InitializeComponent();
            UserInfoPlayer();

            Enemys.Add(new PersonInfo("Злой монстр", 100, 10, 1, 10, 15, 10));
            Enemys.Add(new PersonInfo("Злая рыбеха", 120, 15, 1, 15, 30, 15));

            dispatcherTimer.Tick += AtackPlayer;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            dispatcherTimer.Start();

            SelectEnemy();
        }

        private void AtackPlayer(object sender, EventArgs e)
        {
            Player.Health -= Convert.ToInt32(Enemy.Damage * 100f / (100f - Player.Armor));
            UserInfoPlayer();
        }

        public void SelectEnemy()
        {
            int Id = new Random().Next(0, Enemys.Count);
            Enemy = new PersonInfo(
                Enemys[Id].Name,
                Enemys[Id].Health,
                Enemys[Id].Armor,
                Enemys[Id].Level,
                Enemys[Id].Glasses,
                Enemys[Id].Money,
                Enemys[Id].Damage);
        }

        public void UserInfoPlayer()
        {
            if (Player.Glasses > 100 * Player.Level)
            {
                Player.Level++;
                Player.Glasses = 0;
                Player.Health += 100;
                Player.Damage++;
                Player.Armor++;
            }
            playerHealth.Content = "Жизненные показатели: " + Player.Health;
            playerArmor.Content = "Броня: " + Player.Armor;
            playerLevel.Content = "Уровень: " + Player.Level;
            playerGlasses.Content = "Опыт: " + Player.Glasses;
            playerMoney.Content = "Монеты: " + Player.Money;
        }

        private void AttackEnemy(object sender, MouseButtonEventArgs e)
        {
            Enemy.Health -= Convert.ToInt32(Player.Damage * 100f / (100f - Enemy.Armor));


            InstantStrike();


            if (Enemy.Health <= 0)
            {
                Player.Glasses += Enemy.Glasses;
                Player.Money += Enemy.Money;
                UserInfoPlayer();
                SelectEnemy();
            }
            else
            {
                enemyHealt.Content = "Жизненные показатели: " + Enemy.Health;
                enemyArmor.Content = "Броня: " + Enemy.Armor;
            }
        }




        private void InstantStrike()
        {
            int percent = rnd.Next(100);
            int hits = 1;

            if (percent < 2) // 4 удара шанс 2%
                hits = 4;
            else if (percent < 7) // 3 удара шанс 5%
                hits = 3;
            else if (percent < 27) // 2 удара шанс 20%
                hits = 2;

            countHits.Content = "Ударов за клик: " + hits;

            for (int i = 1; i < hits; i++)
            {
                if (Enemy.Health <= 0) break;
                Enemy.Health -= Convert.ToInt32(Player.Damage * 100f / (100f - Enemy.Armor));
            }
        }

    }
}
