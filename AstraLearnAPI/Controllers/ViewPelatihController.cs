using AstraLearnAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace AstraLearnAPI.Controllers
{
    [ApiController]
    public class ViewPelatihController : Controller
    {
        private readonly ViewPelatihRepository _viewPelatihRepository;

        public ViewPelatihController(IConfiguration configuration)
        {
            _viewPelatihRepository = new ViewPelatihRepository(configuration);
        }

        [HttpGet("[controller]/GetAllPelatihs")]
        public ActionResult<ResponseModel> GetAllPelatihs(int id_pengguna)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                responseModel.message = "Berhasil";
                responseModel.status = 200;
                responseModel.data = _viewPelatihRepository.GetAllData(id_pengguna);
            }
            catch (Exception ex)
            {
                responseModel.message = ex.Message;
                responseModel.status = 500;
            }
            return Ok(responseModel);
        }

    }
}
