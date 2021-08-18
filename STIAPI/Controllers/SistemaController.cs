using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CamaraDeDiputadosLibrary.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STIModel.STI;

namespace STIWebApi.Controllers
{
    //[Route("[controller]")]
    [Route("[controller]/[action]")]
    [ApiController]
    public class SistemaController : ControllerBase
    {
        [HttpGet]
        public Response<List<STIModel.STI.Sistema>> getSistemas()
        {
            Response<List<STIModel.STI.Sistema>> vResult = STIModel.STI.Sistema.getSistemas();
            return vResult;
        }

        [HttpGet]
        [Route("{prmId}")]
        public Response<List<Requerimiento>> getRequerimientos(int prmId)
        {
            Response<List<Requerimiento>> vResult = Requerimiento.getRequerimientos(prmId);
            return vResult;
        }

        [HttpGet]
        public Response<STIModel.STI.Estadistica> getEstadisticas()
        {
            Response<STIModel.STI.Estadistica> vResult = STIModel.STI.Estadistica.getEstadisticas();
            return vResult;
        }

        [HttpGet]
        public Response<List<STIModel.STI.PublicacionDesarrollador>> getPublicaciones()
        {
            Response<List<STIModel.STI.PublicacionDesarrollador>> vResult = STIModel.STI.PublicacionDesarrollador.getPublicaciones();
            return vResult;
        }

        [HttpPost]
        public Response<string> SetSistema(STIModel.STI.Sistema sistema)
        {
            Response<string> vResult = STIModel.STI.Sistema.setSistema(sistema);

            return vResult;
        }
    }
}