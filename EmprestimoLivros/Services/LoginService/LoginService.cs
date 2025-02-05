



using EmprestimoLivros.Data;
using EmprestimoLivros.Dto;
using EmprestimoLivros.Models;
using EmprestimoLivros.Services.SenhaService;
using EmprestimoLivros.Services.SessaoService;

namespace EmprestimoLivros.Services.LoginService
{
    public class LoginService : ILoginInterface
    {
        private readonly AppDbContext _appDbContext;
        private readonly ISenhainterface _senhainterface;
        private readonly ISessaoInterface _sessaoInterface;

        public LoginService(AppDbContext appDbContext, ISenhainterface senhainterface, ISessaoInterface sessaoInterface)
        {
            _appDbContext = appDbContext;
            _senhainterface = senhainterface;
            _sessaoInterface = sessaoInterface;
        }

        public async Task<ResponseModel<UsuarioLoginDto>> Login(UsuarioLoginDto loginDto)
        {
            ResponseModel<UsuarioLoginDto> response = new ResponseModel<UsuarioLoginDto>();

            try
            {

                var usuario =  _appDbContext.usuario.FirstOrDefault(x => x.Email == loginDto.Email);

                if (usuario == null)
                {
                    response.Mensagem = "Credenciais inválidas!";
                    response.Status = false;
                    return response;
                }

                if(!_senhainterface.VerificaSenha(loginDto.Senha, usuario.SenhaHash, usuario.SenhaSalt))
                {
                    response.Mensagem = "Senha incorreta!";
                    response.Status = false;
                    return response;
                }

                //Criar sessão
                _sessaoInterface.CriarSessao(usuario);

                response.Mensagem = "Você esta Logado!";
                return response;

            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
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
