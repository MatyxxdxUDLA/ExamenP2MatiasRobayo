using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ExamenP2MatiasRobayo.Models;

namespace ExamenP2MatiasRobayo
{
    public class RecargaViewModel : INotifyPropertyChanged
    {
        private string operadorSelect;
        private int montoSelect;
        public ObservableCollection<string> Operadora { get; set; }
        public Telefono Telefono { get; set; }

        public Recarga Recarga { get; set; }

        private string _numero;
        public string Numero
        {
            get => _numero;
            set
            {
                if (_numero != value)
                {
                    _numero = value;
                    OnPropertyChanged();
                }
            }
        }

        public string OperadorTipos
        {
            get => operadorSelect;
            set { operadorSelect = value; OnPropertyChanged(); }
        }

        public int MontoSeleccionado
        {
            get => montoSelect;
            set { montoSelect = value; OnPropertyChanged(); }
        }

        public RecargaViewModel()
        {
            Operadora = new ObservableCollection<string>();
            Telefono = new Telefono();
            Recarga = new Recarga();
            LoadOperators();
        }

        public void LoadOperators()
        {
            Operadora.Add("Movistar");
            Operadora.Add("Claro");
            Operadora.Add("Tuenti");
        }

        public async Task ConfirmarRecarga()
        {
            bool confirmacion = await App.Current.MainPage.DisplayAlert("Confirmación", $"¿Desea recargar {MontoSeleccionado} dólares al número {Telefono.numero}?", "Sí", "No");
            if (confirmacion)
            {
                await GuardarTextoRecarga();
                await App.Current.MainPage.DisplayAlert("Éxito", "Recarga exitosa", "OK");
            }
        }

        private async Task GuardarTextoRecarga()
        {
            string folderPath = @"C:\Archivos";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string fileName = $"{Telefono.numero}.txt";
            string fullPath = Path.Combine(folderPath, fileName);

            string texto = $"Se hizo una recarga de {MontoSeleccionado} dólares en la siguiente fecha: {DateTime.Now.ToString("dd/MM/yy")}";

            try
            {
                await File.WriteAllTextAsync(fullPath, texto);
                await App.Current.MainPage.DisplayAlert("Éxito", "Archivo guardado correctamente en 'C:\\Archivos'", "OK");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", "No se pudo guardar el archivo: " + ex.Message, "OK");
            }
        }




        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
