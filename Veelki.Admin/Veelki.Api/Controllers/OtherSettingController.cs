using Microsoft.AspNetCore.Mvc;
using Veelki.Core.IServices;
using Veelki.Data.Entities;
using Veelki.Models.Model;
using System.Threading.Tasks;

namespace Veelki.Api.Controllers
{
    [Route("api/OtherSetting")]
    [ApiController]
    public class OtherSettingController : ControllerBase
    {
        private readonly IOtherSetting _otherSetting;
        public OtherSettingController(IOtherSetting otherSetting)
        {
            _otherSetting = otherSetting;
        }

        [HttpPost, Route("SaveLogo")]
        public async Task<CommonReturnResponse> SaveLogo(Logo model)
        {
            return await _otherSetting.AddUpdateLogoAsync(model);
        }

        [HttpPost, Route("SaveNews")]
        public async Task<CommonReturnResponse> SaveNews(News model)
        {
            return await _otherSetting.AddUpdateNewsAsync(model);
        }

        [HttpPost, Route("SaveSlider")]
        public async Task<CommonReturnResponse> SaveSlider(Slider model)
        {
            return await _otherSetting.AddUpdateSliderAsync(model);
        }

        [HttpGet, Route("DeleteLogo")]
        public async Task<CommonReturnResponse> DeleteLogo(int id)
        {
            return await _otherSetting.DeleteLogoAsync(id);
        }

        [HttpGet, Route("DeleteNews")]
        public async Task<CommonReturnResponse> DeleteNews(int id)
        {
            return await _otherSetting.DeleteNewsAsync(id);
        }

        [HttpGet, Route("DeleteSlider")]
        public async Task<CommonReturnResponse> DeleteSlider(int id)
        {
            return await _otherSetting.DeleteSliderAsync(id);
        }
    }
}
