using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace KontaktyTelefoniczne.Models
{
    public class Kontakt : INotifyPropertyChanged
    {
        private string imie = string.Empty;
        private string nazwisko = string.Empty;
        private string telefon = string.Empty;
        private string email = string.Empty;

        public string Imie
        {
            get => imie;
            set { imie = value; OnPropertyChanged(); OnPropertyChanged(nameof(Inicjaly)); OnPropertyChanged(nameof(KolorAwatara)); }
        }

        public string Nazwisko
        {
            get => nazwisko;
            set { nazwisko = value; OnPropertyChanged(); OnPropertyChanged(nameof(Inicjaly)); OnPropertyChanged(nameof(KolorAwatara)); }
        }

        public string Telefon
        {
            get => telefon;
            set { telefon = value; OnPropertyChanged(); }
        }

        public string Email
        {
            get => email;
            set { email = value; OnPropertyChanged(); }
        }

        public string Inicjaly
        {
            get
            {
                char i = !string.IsNullOrWhiteSpace(Imie) ? Imie[0] : ' ';
                char n = !string.IsNullOrWhiteSpace(Nazwisko) ? Nazwisko[0] : ' ';
                return $"{i}{n}".Trim().ToUpper();
            }
        }

        public Color KolorAwatara
        {
            get
            {
                string[] paleta = { "#E53935", "#D81B60", "#8E24AA", "#3949AB", "#1E88E5", "#00897B", "#43A047", "#FB8C00", "#6D4C41" };
                if (string.IsNullOrEmpty(Inicjaly)) return Color.FromArgb("#191970");
                int hash = Math.Abs(Inicjaly.GetHashCode());
                return Color.FromArgb(paleta[hash % paleta.Length]);
            }
        }

        public Kontakt(string imie, string nazwisko, string telefon, string email)
        {
            Imie = imie;
            Nazwisko = nazwisko;
            Telefon = telefon;
            Email = email;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}