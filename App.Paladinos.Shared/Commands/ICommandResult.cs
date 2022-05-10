namespace App.Paladinos.Shared.Commands
{
    public interface ICommandResult
    {
        bool Sucesso { get; set; }
        string Menssagem { get; set; }
        object Dados { get; set; }
    }
}
