using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CamaraDeDiputadosLibrary.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sistema = STIModel.Sistema;

namespace RemuneracionesWebApi.Controllers
{
    //[Route("[controller]")]
    [Route("[controller]/[action]")]
    [ApiController]
    public class SistemaController : ControllerBase
    {
        [HttpGet]
        public Response<List<Sistema>> getSistemas()
        {
            Response<List<Sistema>> vResult = Sistema.getSistemas();
            return vResult;
        }

        //[HttpGet]
        //[Route("{prmId}")]
        //public Response<List<Sistema>> getSistemaById(int prmId)
        //{
        //    Response<List<Sistema>> vResult = Sistema.getSistemaById(prmId);
        //    return vResult;
        //}

        [HttpPost]
        public Response<string> SetSistema(Sistema sistema)
        {
            Response<string> vResult = Sistema.setSistema(sistema);

            return vResult;
        }
    }
}