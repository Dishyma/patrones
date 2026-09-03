using System;

public abstract class ValidadorBase : IValidador
{
    // Aqui se genera el todo partes
    private IValidador _siguiente;

    public IValidador EstablecerSiguiente(IValidador siguiente)
    {
        _siguiente = siguiente;
        return siguiente; // permite encadenar con fluidez: a.EstablecerSiguiente(b).EstablecerSiguiente(c)
    }

    public bool Validar(SolicitudCredito solicitud)
    {
        if (!ValidarPropio(solicitud))
            return false; // corta la cadena: la solicitud queda rechazada aquí

        // Si no hay más eslabones, la solicitud pasó todas las validaciones
        return _siguiente == null || _siguiente.Validar(solicitud);
    }

    protected abstract bool ValidarPropio(SolicitudCredito solicitud);
}