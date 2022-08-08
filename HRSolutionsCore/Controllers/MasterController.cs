using HRSolutions.BusinessLayer;
using HRSolutionsCore.RequestModel;
using HRSolutionsCore.ResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRSolutionsCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterController : ControllerBase
    {
        public readonly MasterBusiness _masterBusiness;
        public MasterController(MasterBusiness masterBusiness)
        {
            _masterBusiness = masterBusiness;
        }
        #region CategoryMaster
        //Add new category
        [HttpPost]
        [Route("Category")]
        public async Task<IActionResult> Category(MasterCategoryModel req)
        {
            try
            {
                //Validating request body

                var validator = new MasterCategoryValidator();
                var validationResult = await validator.ValidateAsync(req);
                if (!validationResult.IsValid)
                {
                    return BadRequest(new ErrorResponseModel { error = new ErrorModel { code = "400", message = "Validation error", innerError = validationResult.Errors.Select(z => z.ErrorMessage) } });
                }
                var response = _masterBusiness.AddCategory(req);
                if (response.Success == true)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("Category")]
        public IActionResult Category()
        {
            try
            {
                var response = _masterBusiness.GetCategory();
                if (response.Success)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPatch]
        [Route("CategoryStatus")]
        public async Task<IActionResult> CategoryStatus([FromBody] CategoryStatusModel req)
        {
            //Validating request body
            var validator = new CategoryStatusModelValidator();
            var validationResult = await validator.ValidateAsync(req);
            if (!validationResult.IsValid)
            {
                return BadRequest(new ErrorResponseModel { error = new ErrorModel { code = "400", message = "Validation error", innerError = validationResult.Errors.Select(z => z.ErrorMessage) } });
            }
            var response = _masterBusiness.ChangeCategoryStatus(req);
            if (response.Success)
            {
                return Ok(response);
            }
            return Ok();
        }
        #endregion
        #region SubCategoryMaster
        [HttpPost]
        [Route("SubCategory")]
        public async Task<IActionResult> SubCategory(MasterSubCategoryModel req)
        {
            try
            {
                //Validating request body
                var validator = new MasterSubCategoryValidator();
                var validationResult = await validator.ValidateAsync(req);
                if (!validationResult.IsValid)
                {
                    return BadRequest(new ErrorResponseModel { error = new ErrorModel { code = "400", message = "Validation error", innerError = validationResult.Errors.Select(z => z.ErrorMessage) } });
                }
                var response = _masterBusiness.AddSubCategory(req);
                if (response.Success == true)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
