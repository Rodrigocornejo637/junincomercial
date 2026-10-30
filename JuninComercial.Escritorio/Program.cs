using JuninComercial.Escritorio.Vistas;

namespace JuninComercial.Escritorio;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new FrmLocationLista());
    }
}