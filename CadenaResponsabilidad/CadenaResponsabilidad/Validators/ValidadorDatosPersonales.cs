using System;

public class ValidadorDatosPersonales : ValidadorBase
{
    protected override bool ValidarPropio(SolicitudCredito solicitud)
    {
        bool ok = !string.IsNullOrWhiteSpace(solicitud.Nombre) &&
                   !string.IsNullOrWhiteSpace(solicitud.Cedula);
        Console.WriteLine(ok ? "Datos personales completos."
                              : "Datos personales incompletos.");
        return ok;
    }
}