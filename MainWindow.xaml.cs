using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Lab1
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string plikArchiwizacji = "archiwum.txt";


        public MainWindow()
        {
            TextBoxWithErrorProvider.BrushForAll = Brushes.Red;
            InitializeComponent();
            forename.SetFocus();

        }


        private bool isValid(TextBoxWithErrorProvider tb)
        {
            if (tb.Text.Trim() != "" & tb.Text.All(char.IsLetter))
            {
                tb.SetError("");
                return true;
            }
            if (tb.Text.Trim() == "")
            {
                tb.SetError("Pole nie może być puste");
                return false;
            }
            tb.SetError("Pole nie może zawierać cyfr");
            return false;
        }

        private bool isValid(Slider slider)
        {
            if (slider.Value == 0)
            {
                slider.ToolTip = "Wartość nie może być równa zero";
                slider.BorderThickness = new Thickness(1);
                return false;
            }
            slider.BorderThickness = new Thickness(0);
            slider.ToolTip = "";
            return true;

        }

        private void Clear()
        {
            forename.Text = surname.Text = "";
            sliderAge.Value = 20;
            sliderWeight.Value = 75;
            listBoxPilkarze.SelectedIndex = -1;
            buttonEdytuj.IsEnabled = false ;
            buttonUsun.IsEnabled = false;
            forename.SetFocus();
            
        }
        private bool isDuplicate(Pilkarz currentPlayer)
        {
            foreach (var p in listBoxPilkarze.Items)
            {
                var pilkarz = p as Pilkarz;
                if (pilkarz.isTheSame(currentPlayer))
                {
                   return true;
                }
            }
            return false;
        }

        private void Load(Pilkarz player)
        {
            forename.Text = player.Forename;
            surname.Text = player.Surname;
            sliderAge.Value = player.Age;
            sliderWeight.Value = player.Weight;
            forename.Focus();
            buttonEdytuj.IsEnabled = true;
            buttonUsun.IsEnabled = true;
        }

        private void buttonDodaj_Click(object sender, RoutedEventArgs e)
        {
            // var button = sender as Button;
            if (isValid(forename) & isValid(surname) & isValid(sliderAge) & isValid(sliderWeight))
            {
                var newPlayer = new Pilkarz(forename.Text, surname.Text, sliderAge.Value, sliderWeight.Value);
                if (!isDuplicate(newPlayer))
                {
                    listBoxPilkarze.Items.Add(newPlayer);
                    Clear();
                }
                else
                {
                    var dialog = MessageBox.Show($"{newPlayer.ToString()} już jest na liście {Environment.NewLine} Czy wyczyścić formularz?", "Uwaga", MessageBoxButton.OKCancel);
                    if (dialog == MessageBoxResult.OK)
                    {
                        Clear();
                    }
                }
               
            }


            // var button = sender as Button; // to jest referencja, jezeli button to zrzuca a jak nie to null ?? zabezpieczenie null
            // var button=(Button)sender tutaj jak nie zrzutuje to wywali wyjatek

            // textBoxName.Text = button.Content.ToString();// wyciąganie tego kto jest senderem
        }

        private void buttonEdytuj_Click(object sender, RoutedEventArgs e)
        {
            var biezacyPilkarz = new Pilkarz(forename.Text.Trim(), surname.Text.Trim(), (uint)sliderAge.Value, (uint)sliderWeight.Value);

            if (!isDuplicate(biezacyPilkarz))
            {
                var dialogResult = MessageBox.Show($"Czy na pewno chcesz zmienić dane  {Environment.NewLine} {listBoxPilkarze.SelectedItem}?", "Edycja", MessageBoxButton.YesNo);

                if (dialogResult == MessageBoxResult.Yes)
                {
                    //zamiana refernecji do obiektu piłkarza edytowanego
                    //zmień implementację tak aby zmieniać stan obiektu a nie podmieniać referencję
                    //zrobione
                    Pilkarz selected = listBoxPilkarze.Items[listBoxPilkarze.SelectedIndex] as Pilkarz;
                    selected.Copy(biezacyPilkarz);
                    listBoxPilkarze.Items.Refresh();


                }
                Clear();
                listBoxPilkarze.SelectedIndex = -1;

            }
            else
                MessageBox.Show($"{biezacyPilkarz.ToString()} już jest na liście.", "Uwaga");

        }

        private void buttonUsun_Click(object sender, RoutedEventArgs e)
        {

            //zaimplementuj mechanizm usuwania zaznaczonej pozycji na liście
            //zapytaj czy napewno usunąć

            var dialogResult = MessageBox.Show($"Czy na pewno chcesz usunąć {Environment.NewLine} {listBoxPilkarze.SelectedItem}?", "Usuń", MessageBoxButton.YesNo);

            if (dialogResult == MessageBoxResult.Yes)
            {
                
                Pilkarz selected = listBoxPilkarze.Items[listBoxPilkarze.SelectedIndex] as Pilkarz;

                listBoxPilkarze.Items.Remove(selected);


            }

        }


        private void listBoxPilkarze_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            if (listBoxPilkarze.SelectedIndex > -1)
            {
                Load((Pilkarz)listBoxPilkarze.SelectedItem);
            }

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            int n = listBoxPilkarze.Items.Count;
            Pilkarz[] pilkarze = null;
            if (n > 0)
            {
                pilkarze = new Pilkarz[n];
                int index = 0;
                foreach (var o in listBoxPilkarze.Items)
                {
                    pilkarze[index++] = o as Pilkarz;
                }
                Archiwizacja.ZapisPilkarzyDoPliku(plikArchiwizacji, pilkarze);
            }


        }
        //metoda wykonana po załadowaniu okna
        //ładujemy zawartość pliku z zapisanymi piłkarzami jeśli tylko istnieje
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var pilkarze = Archiwizacja.CzytajPilkarzyZPliku(plikArchiwizacji);
            if (pilkarze != null)
                foreach (var p in pilkarze)
                {
                    listBoxPilkarze.Items.Add(p);
                }

        }
    }
}