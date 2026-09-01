using System;

public class ValidadorHistorialCrediticio : ValidadorBase
{
    protected override bool ValidarPropio(SolicitudCredito solicitud)
    {
        bool ok = solicitud.PuntajeCrediticio >= 600;
        Console.WriteLine(ok ? "Historial crediticio aceptable."
                              : "Historial crediticio insuficiente.");
        return ok;
    }
}