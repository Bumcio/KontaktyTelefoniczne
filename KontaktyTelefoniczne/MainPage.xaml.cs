using KontaktyTelefoniczne.Models;
using System.Collections.ObjectModel;

namespace KontaktyTelefoniczne
{
    public partial class MainPage : ContentPage
    {
        private ObservableCollection<Kontakt> WszytskieKontakty { get; set; } = new();

        public ObservableCollection<Kontakt> WyswietlaneKontakty { get; set; } = new();

        public MainPage()
        {
            InitializeComponent();
            WszytskieKontakty.Add(new Kontakt("Jan", "Kowalski", "+48 600 123 456", "jan@example.com"));
            WszytskieKontakty.Add(new Kontakt("Anna", "Nowak", "+48 700 987 654", "anna@example.com"));

            OdswiezListe();

            BindingContext = this;
            KontaktyCollectionView.ItemsSource = WyswietlaneKontakty;
        }

        private void OdswiezListe(string? filtr = null)
        {
            var posortowane = WszytskieKontakty.OrderBy(k => k.Nazwisko).ThenBy(k => k.Imie);

            if (!string.IsNullOrWhiteSpace(filtr))
            {
                posortowane = posortowane
                    .Where(k => k.Imie.Contains(filtr, StringComparison.OrdinalIgnoreCase) ||
                                k.Nazwisko.Contains(filtr, StringComparison.OrdinalIgnoreCase))
                    .OrderBy(k => k.Nazwisko)
                    .ThenBy(k => k.Imie);
            }

            WyswietlaneKontakty.Clear();
            foreach (var kontakt in posortowane)
            {
                WyswietlaneKontakty.Add(kontakt);
            }
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            OdswiezListe(e.NewTextValue);
        }
        private void OnDodajKontaktClicked(object sender, EventArgs e)
        {
            FormularzFrame.IsVisible = true;
        }

        private void OnZapiszClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EntryImie.Text) || string.IsNullOrWhiteSpace(EntryNazwisko.Text))
            {
                DisplayAlert("Błąd", "Imię i nazwisko są wymagane!", "OK");
                return;
            }

            var nowy = new Kontakt(EntryImie.Text, EntryNazwisko.Text, EntryTelefon.Text, EntryEmail.Text);
            WszytskieKontakty.Add(nowy);

            EntryImie.Text = string.Empty;
            EntryNazwisko.Text = string.Empty;
            EntryTelefon.Text = string.Empty;
            EntryEmail.Text = string.Empty;
            FormularzFrame.IsVisible = false;

            OdswiezListe();
        }

        private void OnAnulujClicked(object sender, EventArgs e)
        {
            FormularzFrame.IsVisible = false;
        }

        private void OnUsunClicked(object sender, EventArgs e)
        {
            if (sender is ImageButton button && button.BindingContext is Kontakt kontakt)
            {
                WszytskieKontakty.Remove(kontakt);
                OdswiezListe();
            }
        }
    }
}