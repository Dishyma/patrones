public interface IValidador
{
    IValidador EstablecerSiguiente(IValidador siguiente);
    bool Validar(SolicitudCredito solicitud);
}