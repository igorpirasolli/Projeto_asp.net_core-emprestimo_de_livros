namespace EmprestimoLivros.Services.SenhaService
{
    public interface ISenhainterface
    {
        void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt);
    }
}
