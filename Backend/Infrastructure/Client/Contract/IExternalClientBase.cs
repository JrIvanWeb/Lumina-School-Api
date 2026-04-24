namespace LuminiSchool.Infrastructure.Client.Contract
{
    /// <summary>
    /// Contrato base para clientes de APIs externas.
    /// Implementar por cada servicio externo (correo, SMS, IA, etc.)
    /// </summary>
    public interface IExternalClientBase
    {
        Task<bool> PingAsync();
    }
}
