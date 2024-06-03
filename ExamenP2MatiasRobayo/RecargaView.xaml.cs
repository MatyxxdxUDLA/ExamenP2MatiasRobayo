namespace ExamenP2MatiasRobayo;

public partial class RecargaView : ContentPage
{
    private RecargaViewModel viewModel;
	public RecargaView()
	{
		InitializeComponent();
        viewModel = new RecargaViewModel();
        this.BindingContext = viewModel;
	}
    private void MRRecargaClicked(object sender, EventArgs e)
    {
        viewModel.ConfirmarRecarga();
    }
    private void OnAmountChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            RadioButton rb = (RadioButton)sender;
            int montoSeleccionado = int.Parse(rb.Content.ToString());
            viewModel.MontoSeleccionado = montoSeleccionado;
            DisplayAlert("Selección de recarga", $"Ha seleccionado una recarga de: {montoSeleccionado} dólares", "OK");
        }
    }


}