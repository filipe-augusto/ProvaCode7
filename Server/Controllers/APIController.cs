using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ProvaCode7.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProvaCode7.Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class APIController : Controller
    {
        private readonly AppDbContext _context;
        private IMapper _mapper;
        public APIController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        //[HttpGet("BuscaCliente")]
        //public async Task<ActionResult<List<ClienteListModelView>>> BuscaCliente([FromQuery] string palavra)
        //{

        //}

        }
}
