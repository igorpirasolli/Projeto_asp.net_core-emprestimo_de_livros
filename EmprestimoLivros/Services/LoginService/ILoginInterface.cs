using EmprestimoLivros.Dto;
using EmprestimoLivros.Models;

namespace EmprestimoLivros.Services.LoginService
{
    public interface ILoginInterface
    {
        Task<ResponseModel<UsuarioModel>> Registrar(UsuarioRegisterDto registerDto);

        Task<ResponseModel<UsuarioLoginDto>> Login(UsuarioLoginDto loginDto);
    }
}
