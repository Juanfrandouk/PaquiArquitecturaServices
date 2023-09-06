using Microsoft.AspNetCore.Mvc;
using Paquigroup.Ecommerce.Application.DTO;
using Paquigroup.Ecommerce.Application.Interface;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Paquigroup.Ecommerce.Services.WebApi.Controllers
{
    //[Authorize]
    [Route("api/[controller]/[action]")]
    [Consumes("application/json")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerApplication _customerApplication;
        public CustomersController(ICustomerApplication customerApplication)
        {
            _customerApplication = customerApplication;
        }
        #region Métodos Sincronos
        // GET: api/<ValuesController>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet("{customerId}")]
        public IActionResult Get(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
                return BadRequest();
            var response = _customerApplication.Get(customerId);
            if (response.IsSuccess)
                return Ok(response);
            return BadRequest(response.Message);
        }
        // POST api/<ValuesController>
        /// <summary>
        /// Crea un registro para cliente
        /// </summary>
        /// <param name="customerDto"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Insert([FromBody] CustomersDto customerDto)
        {
            if (customerDto == null)
                return BadRequest();
            var response = _customerApplication.Insert(customerDto);
            if (response.IsSuccess)
                return Ok(response);
            return BadRequest(response.Message);

        }

        // PUT api/<ValuesController>/5
        /// <summary>
        /// Actualiza un registro de cliente
        /// </summary>
        /// <param name="customerDto"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Update([FromBody] CustomersDto customerDto)
        {
            if (customerDto == null)
                return BadRequest();
            var response = _customerApplication.Update(customerDto);
            if (response.IsSuccess)
                return Ok(response);
            return BadRequest(response.Message);
        }

        // DELETE api/<ValuesController>/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpDelete("{customerId}")]
        public IActionResult Delete(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
                return BadRequest();
            var response = _customerApplication.Delete(customerId);
            if (response.IsSuccess)
                return Ok(response);
            return BadRequest(response.Message);
        }

        // GET api/<ValuesController>/5

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = _customerApplication.GetAll();
            if (response.IsSuccess)
                return Ok(response);
            return BadRequest(response.Message);
        }
        #endregion


        #region Métodos Asincronos


        /// <summary>
        /// Crea un registro para cliente
        /// </summary>
        /// <param name="customerDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("InserAsync")]
        public async Task<IActionResult> InserAsync([FromBody] CustomersDto customerDto)
        {
            if (customerDto == null)
                return BadRequest();
            var response = await _customerApplication.InsertAsync(customerDto);
            if (response.IsSuccess)
                return Ok(response);
            return BadRequest(response.Message);

        }

        /// <summary>
        /// Actualiza un registro de cliente
        /// </summary>
        /// <param name="customerDto"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync([FromBody] CustomersDto customerDto)
        {
            if (customerDto == null)
                return BadRequest();
            var response = await _customerApplication.UpdateAsync(customerDto);
            if (response.IsSuccess)
                return Ok(response);
            return BadRequest(response.Message);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        [Route("DeleteAsync")]
        public async Task<IActionResult> DeleteAsync(string Id)
        {
            if (string.IsNullOrEmpty(Id))
                return BadRequest();
            var response = await _customerApplication.DeleteAsync(Id);
            if (response.IsSuccess)
                return Ok(response);
            return BadRequest(response.Message);
        }

        /// <summary>
        /// Devuelveel registro de un clientes y la operacion la ejecuta de forma asincrona
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        [Route("GetAsync")]
        public async Task<IActionResult> GetAsync(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
                return BadRequest();
            var response = await _customerApplication.GetAsync(customerId);
            if (response.IsSuccess)
                return Ok(response);
            return BadRequest(response.Message);
        }


        /// <summary>
        /// Devuelve un listado de todos los clientes y la operacion la ejecuta de forma asincrona
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Route("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _customerApplication.GetAllAsync();
            if (response.IsSuccess)
                return Ok(response);
            return BadRequest(response.Message);
        }


        #endregion
    }
}
