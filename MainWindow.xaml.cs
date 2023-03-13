using System;
using System.Collections.Generic;
using System.IO;
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
using System.Collections;
using System.Collections.ObjectModel;
using Microsoft.Win32;

namespace WpfOsztalyzas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string fajlNev;
        string activeFileName;
        string numberOfNotes;
        double average;
        //Így minden metódus fogja tudni használni.
        ObservableCollection<Osztalyzat> jegyek = new();
        OpenFileDialog openDialog = new();
        SaveFileDialog saveDialog = new();

        public MainWindow()
        {
            InitializeComponent();
            openDialog.FileName = "naplo";
            openDialog.DefaultExt = "csv";
            activeFileName = openDialog.SafeFileName;


            if (openDialog.ShowDialog() == true)
            {


                jegyek.Clear();
                StreamReader sr = new StreamReader(openDialog.FileName);
                while (!sr.EndOfStream)
                {
                    string[] mezok = sr.ReadLine().Split(";");
                    Osztalyzat ujJegy = new Osztalyzat(mezok[0], mezok[1], mezok[2], int.Parse(mezok[3]));
                    jegyek.Add(ujJegy);

                    average += double.Parse(mezok[3]);
                    

                }
                sr.Close(); 
                dgJegyek.ItemsSource = jegyek;

                lbFileName.Content = $"A fájl neve: {activeFileName}";

                numberOfNotes = jegyek.Count.ToString();

                lbNoteCounter.Content = $"A jegyek száma: {numberOfNotes}";
                lbAverage.Content = $"A jegyek átlaga: {average / Convert.ToDouble(numberOfNotes)}";
            }




            // todo Fájlok kitallózásával tegye lehetővé a naplófájl kiválasztását!
            // Ha nem választ ki semmit, akkor "naplo.csv" legyen az állomány neve. A későbbiekben ebbe fog rögzíteni a program.

            // todo A kiválasztott naplót egyből töltse be és a tartalmát jelenítse meg a datagrid-ben!
        }

        private void btnRogzit_Click(object sender, RoutedEventArgs e)
        {
            fajlNev = openDialog.FileName;
            //TODO JAVÍTANI
            if (txtNev.Text.Length >= 7 && txtNev.Text.Contains(" ") && datDatum.SelectedDate <= DateTime.Now)
            {
                string csvSor = $"{txtNev.Text};{datDatum.Text};{cboTantargy.Text};{sliJegy.Value}";
                //Megnyitás hozzáfűzéses írása (APPEND)
                StreamWriter sw = new StreamWriter(fajlNev, append: true);
                sw.WriteLine(csvSor);
                sw.Close();

                Osztalyzat currentNote = new Osztalyzat(txtNev.Text, datDatum.Text, cboTantargy.Text, Convert.ToInt32(sliJegy.Value));
                jegyek.Add(currentNote);
                dgJegyek.ItemsSource = jegyek;

                lbFileName.Content = $"A fájl neve: {activeFileName}";

                numberOfNotes = jegyek.Count.ToString();

                lbNoteCounter.Content = $"A jegyek száma: {numberOfNotes}";
                lbAverage.Content = $"A jegyek átlaga: {average / Convert.ToDouble(numberOfNotes)}";

            }
        }



            //todo Ne lehessen rögzíteni, ha a következők valamelyike nem teljesül!
            // a) - A név legalább két szóból álljon és szavanként minimum 3 karakterből!
            //      Szó = A szöközökkel határolt karaktersorozat.
            // b) - A beírt dátum újabb, mint a mai dátum

            //todo A rögzítés mindig az aktuálisan megnyitott naplófájlba történjen!


            //A CSV szerkezetű fájlba kerülő sor előállítása

            //todo Az újonnan felvitt jegy is jelenjen meg a datagrid-ben!


        private void btnBetolt_Click(object sender, RoutedEventArgs e)
        {

            fajlNev = openDialog.FileName;
            if (openDialog.ShowDialog() == true) 
            { 
            
                jegyek.Clear();  //A lista előző tartalmát töröljük
                StreamReader sr = new StreamReader(fajlNev); //olvasásra nyitja az állományt
                while (!sr.EndOfStream) //amíg nem ér a fájl végére
                {
                    string[] mezok = sr.ReadLine().Split(";"); //A beolvasott sort feltördeli mezőkre
                                                               //A mezők értékeit felhasználva létrehoz egy objektumot
                    Osztalyzat ujJegy = new Osztalyzat(mezok[0], mezok[1], mezok[2], int.Parse(mezok[3]));
                    jegyek.Add(ujJegy); //Az objektumot a lista végére helyezi
                }
                sr.Close(); //állomány lezárása

                //A Datagrid adatforrása a jegyek nevű lista lesz.
                //A lista objektumokat tartalmaz. Az objektumok lesznek a rács sorai.
                //Az objektum nyilvános tulajdonságai kerülnek be az oszlopokba.
                dgJegyek.ItemsSource = jegyek;

                lbFileName.Content = $"A fájl neve: {activeFileName}";

                numberOfNotes = jegyek.Count.ToString();

                lbNoteCounter.Content = $"A jegyek száma: {numberOfNotes}";
                lbAverage.Content = $"A jegyek átlaga: {average / Convert.ToDouble(numberOfNotes)}";
            }
        }

        //todo Felület bővítése: Az XAML átszerkesztésével biztosítsa, hogy láthatóak legyenek a következők!
        // - A naplófájl neve
        // - A naplóban lévő jegyek száma
        // - Az átlag

        //todo Új elemek frissítése: Figyeljen rá, ha új jegyet rögzít, akkor frissítse a jegyek számát és az átlagot is!

        //todo Helyezzen el alkalmas helyre 2 rádiónyomógombot!
        //Feliratok: [■] Vezetéknév->Keresztnév [O] Keresztnév->Vezetéknév
        //A táblázatban a név azserint szerepeljen, amit a rádiónyomógomb mutat!
        //A feladat megoldásához használja fel a ForditottNev metódust!
        //Módosíthatja az osztályban a Nev property hozzáférhetőségét!
        //Megjegyzés: Felételezzük, hogy csak 2 tagú nevek vannak
    }
}

