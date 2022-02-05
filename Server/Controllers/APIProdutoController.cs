using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ProvaCode7.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace ProvaCode7.Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class APIProdutoController : Controller
    {
        private readonly AppDbContext _context;
        private IMapper _mapper;
        public APIProdutoController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet("BuscaVendasPorCpf")]
       // public async Task<JsonResult> BuscaVendasPorCpf([FromQuery] string cpf)
        public async Task<ActionResult<APIConsultaDeVendasViaCliente>> BuscaVendasPorCpf([FromQuery] string cpf)
        {
            try
            {
                var cliente = _context.Cliente.First(x => x.Cpf.Replace(".", "").Replace("-", "").ToLower()
              .Contains(cpf.Replace(".", "").Replace("-", "").ToLower()));
               var listaProdutosVendidos =  _context.ProdutoOfertadoCliente.Where(x => x.IdCliente == cliente.Id).ToList();

                APIConsultaDeVendasViaCliente resultado = new APIConsultaDeVendasViaCliente();
                resultado.Vendas = new List<APIVendasListModel>();
                resultado.QuantidadeDeVendas = listaProdutosVendidos.Count();
                resultado.NomeCliente = cliente.Nome;
                resultado.CPFCliente = cliente.Cpf;
                if (resultado.QuantidadeDeVendas > 0)
                {
                    foreach (var item in listaProdutosVendidos)
                    {
                        var produto = _context.Produto.First(x => x.Id == item.IdProduto);
                        APIVendasListModel produtoVendido = new APIVendasListModel();
                        produtoVendido.CodigoVenda = item.Id.ToString();
                        produtoVendido.DiaVenda = item.DiaDaOferta;
                        produtoVendido.Valor = produto.Valor;
                        produtoVendido.NomePruduto = produto.Descricao;
                        resultado.Vendas.Add(produtoVendido);
                    }
                }

                resultado.ValorTotal = resultado.Vendas.Sum(x => x.Valor);

                return Json(resultado);
            }
            catch (Exception ex)
            {
                return Json("Erro para rotornar os dados");
            }
        }

        [HttpGet("BuscaVendasPorNomeProduto")]
        //public async Task<JsonResult> BuscaVendasPorNomeProduto([FromQuery] string nomeProduto)
        public async Task<ActionResult<APIConsultaDeVendasViaCliente>> BuscaVendasPorNomeProduto([FromQuery] string nomeProduto)
        {
            try { 
              
                var produto = _context.Produto.First(x => x.Descricao.Contains(nomeProduto));
                var listaProdutosVendidos = _context.ProdutoOfertadoCliente.Where(x => x.IdCliente == produto.Id).ToList();

                APIConsultaDeVendasViaProduto resultado = new APIConsultaDeVendasViaProduto();
                resultado.Vendas = new List<APIVendasListModel>();
                resultado.QuantidadeDeVendas = listaProdutosVendidos.Count();
                resultado.ValorProduto = produto.Valor;
                resultado.NomeProduto = produto.Descricao;
                if (resultado.QuantidadeDeVendas > 0)
                {
                    foreach (var item in listaProdutosVendidos)
                    {
                        var produto1 = _context.Produto.First(x => x.Id == produto.Id);
                        APIVendasListModel produtoVendido = new APIVendasListModel();
                        produtoVendido.CodigoVenda = item.Id.ToString();
                        produtoVendido.DiaVenda = item.DiaDaOferta;
                        produtoVendido.Valor = produto.Valor;
                        produtoVendido.NomePruduto = produto.Descricao;
                        resultado.Vendas.Add(produtoVendido);
                    }
                }

                resultado.ValorTotal = resultado.Vendas.Sum(x => x.Valor);

                return Json(resultado);
            }
            catch (Exception ex)
            {
                return Json("Erro para rotornar os dados");
            }
        }



        [HttpPost]
        public async Task<ActionResult<RetornoRequisicao>> Post(InsereProdutoPost produto)
     //   public async Task<JsonResult> Post(InsereProdutoPost produto)
        {

            //IDTipoSoftware vem atraves de um select do tipo byte
            if (produto.Token == "Code7Token1565164169618")
            {


                if (!string.IsNullOrWhiteSpace(produto.NomeProduto))
                {
                    return Json("Nome vazio. Não foi possivel cadastrar produto.");
                }
                if (_context.Produto.Count(x => x.Descricao.Contains(produto.NomeProduto)) > 0)
                {
                    return Json("Já existe um produto com esse nome.");
                }
                decimal valorProduto = 0;
                try
                {
                    valorProduto = Convert.ToDecimal(produto.Valor);
                }
                catch 
                {
                    return Json("Valor incorreto.");
                }

                if (produto.IdCategoria != (byte)EnumTipoProduto.Hardware | produto.IdCategoria != (byte)EnumTipoProduto.Software)
                {
                    return Json("Categoria incorreta");
                }

                try
                {
                    Produto registro = new Produto();
                    registro.Descricao = produto.NomeProduto;
                    registro.Valor = valorProduto;
                    registro.DataCadastro = DateTime.Now;
                    registro.IdCategoria = produto.IdCategoria;

                    await _context.AddAsync(produto);
                    await _context.SaveChangesAsync();
                    return Json("Ok");
                }
                catch (Exception ex)
                {

                    return Json($"Erro para cadastrar o produto. Detalhes: {ex.Message}");
                }
        

            }
            else
            {
                return Json("Token Invalido!");
            }



        }


    }
}
