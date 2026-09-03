using System;

public class ValidadorMontoMaximo : ValidadorBase
{
    private const decimal LimiteSucursal = 50_000_000m;

    protected override bool ValidarPropio(SolicitudCredito solicitud)
    {
        bool ok = solicitud.MontoSolicitado <= LimiteSucursal;
        Console.WriteLine(ok ? "Monto dentro del límite autorizado."
                              : "Monto supera el límite autorizado por la sucursal.");
        return ok;
    }
}