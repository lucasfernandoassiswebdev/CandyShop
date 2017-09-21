using CandyShop.Application.ViewModels;
using System.Collections.Generic;

namespace CandyShop.Application.Interfaces
{
    public interface ICompraInterface
    {
        Response<string> InserirCompra(Compra compra);
        Response<string> EditarCompra(Compra compra);
        Response<string> InserirItens(Produto produto);
        Response<IEnumerable<Compra>> ListaCompra();
        Response<IEnumerable<Compra>> ListaCompraPorCpf(string cpf);
        Response<Compra> SelecionarCompra(int idcompra);
        Response<string> EditaItens(CompraProduto compraProduto);
        Response<IEnumerable<Compra>> ListaCompraPorNome(string nomeUsuario);



    }
}