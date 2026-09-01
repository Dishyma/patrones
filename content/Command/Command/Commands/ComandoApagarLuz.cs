public class ComandoApagarLuz : IComando
{
    private readonly Luz _luz;
    public ComandoApagarLuz(Luz luz) => _luz = luz;

    public void Ejecutar() => _luz.Apagar();
    public void Deshacer() => _luz.Encender();
}