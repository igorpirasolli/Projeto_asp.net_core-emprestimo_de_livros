



using EmprestimoLivros.Data;
using EmprestimoLivros.Dto;
using EmprestimoLivros.Models;
using EmprestimoLivros.Services.SenhaService;

namespace EmprestimoLivros.Services.LoginService
{
    public class LoginService : ILoginInterface
    {
        private readonly AppDbContext _appDbContext;
        private readonly ISenhainterface _senhainterface;

        public LoginService(AppDbContext appDbContext, ISenhainterface senhainterface)
        {
            _appDbContext = appDbContext;
            _senhainterface = senhainterface;
        }
        public async Task<ResponseModel<UsuarioModel>> Registrar(UsuarioRegisterDto registerDto)
        {
            ResponseModel<UsuarioModel> responseModel = new ResponseModel<UsuarioModel>();

            try
            {
                if (VerificarSeEmailExiste(registerDto))
                {
                    responseModel.Mensagem = "Email já cadastrado";
                    responseModel.Status = false;
                    return responseModel;
                }

                _senhainterface.CriarSenhaHash(registerDto.Senha, out byte[] senhaHash, out byte[] senhaSalt);

                var usuario = new UsuarioModel
                {
                    Nome = registerDto.Nome,
                    Sobrenome = registerDto.Sobrenome,
                    Email = registerDto.Email,
                    SenhaHash = senhaHash,
                    SenhaSalt = senhaSalt


                };

                _appDbContext.Add(usuario);
                await _appDbContext.SaveChangesAsync();

                responseModel.Mensagem = "Usuário cadastrado com sucesso!";

                return responseModel;
                


            }
            catch (Exception ex)
            {

                responseModel.Mensagem = ex.Message;
                responseModel.Status = false;
                return responseModel;
            }


        }

        private bool VerificarSeEmailExiste(UsuarioRegisterDto registerDto)
        {
            var usuario = _appDbContext.usuario.FirstOrDefault(x => x.Email == registerDto.Email);

            if (usuario == null)
            {
                return false;
            }

            return true;
        }
    }
}
