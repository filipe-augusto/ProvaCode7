using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using ProvaCode7.Models;
using ProvaCode7.Shared;
using ProvaCode7.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ProvaCode7.Server
{

    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : Controller
    {

        private readonly AppDbContext _context;
        private IMapper _mapper;
        public ClienteController(AppDbContext context, , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }




        [HttpGet("PegaLista")]
        public async Task<ActionResult<List<ClienteListModelView>>> PegaLista()
        {
            try
            {
                //AsNoTracking busca mais rapida
                var listaClientes = _context.Cliente.Take(20).AsNoTracking().ToList();

                List<ClienteListModelView> ListaClientesModel = new List<ClienteListModelView>();
                //O MAPER PASSA A ENTIDADE PARA MODEL SEM A NECESSIDADE DE PASSAR VIA FOREACH
                ListaClientesModel = _mapper.Map<List<ClienteListModelView>>(listaClientes);

                return Ok(_mapper.Map<List<ClienteListModelView>>(listaClientes));
            }
            catch (Exception ex)
            {
                return BadRequest(new RetornoRequisicao { Mensagem = ex.Message, Sucesso = false });
            }
        }

        [HttpPost]
        public async Task<ActionResult<RetornoRequisicao>> Post(ClienteViewModel clienteViewModel)
        {
            try
            {

                using (IDbContextTransaction dbContextTransaction = _context.Database.BeginTransaction())
                {
                    Cliente cliente = new Cliente();
                    Endereco endereco = new Endereco();
                    cliente = _mapper.Map<Cliente>(clienteViewModel);
                    endereco = _mapper.Map<Endereco>(clienteViewModel.Endereco);

                    try
                    {
                        await _context.AddAsync(endereco);
                        await _context.SaveChangesAsync();

                        cliente.IdEndereco = endereco.Id;
                        await _context.AddAsync(cliente);
                        await _context.SaveChangesAsync();

                        await dbContextTransaction.CommitAsync();
                        return Ok(new RetornoRequisicao { Sucesso = true, Mensagem = "Cliente cadastrado com sucesso!" });
                    }
                    catch (Exception ex)
                    {
                        await dbContextTransaction.RollbackAsync();
                        return BadRequest(new RetornoRequisicao { Mensagem = ex.Message, Sucesso = false });
                    }
                }
            }
            catch (Exception ex)
            {

                return BadRequest(new RetornoRequisicao() { Mensagem = ex.Message, Sucesso = false });
            }
        }




    }
}
