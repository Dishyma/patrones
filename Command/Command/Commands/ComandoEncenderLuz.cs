// ---------- Comandos concretos ----------
public class ComandoEncenderLuz : IComando
{
    private readonly Luz _luz;
    public ComandoEncenderLuz(Luz luz) => _luz = luz;

    public void Ejecutar() => _luz.Encender();
    public void Deshacer() => _luz.Apagar();
}