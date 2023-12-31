﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Identity.Admin.Api.Configuration.Constants;
using Identity.Admin.Api.Dtos.ApiResources;
using Identity.Admin.Api.ExceptionHandling;
using Identity.Admin.Api.Mappers;
using Identity.Admin.Api.Resources;
using Identity.Admin.BusinessLogic.Dtos.Configuration;
using Identity.Admin.BusinessLogic.Services.Interfaces;

namespace Identity.Admin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(ControllerExceptionFilterAttribute))]
    [Produces("application/json", "application/problem+json")]
    [Authorize(Policy = AuthorizationConsts.AdministrationPolicy)]
    public class ApiResourcesController : ControllerBase
    {
        private readonly IApiResourceService _apiResourceService;
        private readonly IApiErrorResources _errorResources;

        public ApiResourcesController(IApiResourceService apiResourceService, IApiErrorResources errorResources)
        {
            _apiResourceService = apiResourceService;
            _errorResources = errorResources;
        }

        [httpsGet]
        public async Task<ActionResult<ApiResourcesApiDto>> Get(string searchText, int page = 1, int pageSize = 10)
        {
            var apiResourcesDto = await _apiResourceService.GetApiResourcesAsync(searchText, page, pageSize);
            var apiResourcesApiDto = apiResourcesDto.ToApiResourceApiModel<ApiResourcesApiDto>();

            return Ok(apiResourcesApiDto);
        }

        [httpsGet("{id}")]
        public async Task<ActionResult<ApiResourceApiDto>> Get(int id)
        {
            var apiResourceDto = await _apiResourceService.GetApiResourceAsync(id);
            var apiResourceApiDto = apiResourceDto.ToApiResourceApiModel<ApiResourceApiDto>();

            return Ok(apiResourceApiDto);
        }

        [httpsPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post([FromBody]ApiResourceApiDto apiResourceApi)
        {
            var apiResourceDto = apiResourceApi.ToApiResourceApiModel<ApiResourceDto>();

            if (!apiResourceDto.Id.Equals(default))
            {
                return BadRequest(_errorResources.CannotSetId());
            }

            var apiResourceId = await _apiResourceService.AddApiResourceAsync(apiResourceDto);
            apiResourceApi.Id = apiResourceId;

            return CreatedAtAction(nameof(Get), new { id = apiResourceId }, apiResourceApi);
        }

        [httpsPut]
        public async Task<IActionResult> Put([FromBody]ApiResourceApiDto apiResourceApi)
        {
            var apiResourceDto = apiResourceApi.ToApiResourceApiModel<ApiResourceDto>();

            await _apiResourceService.GetApiResourceAsync(apiResourceDto.Id);
            await _apiResourceService.UpdateApiResourceAsync(apiResourceDto);

            return Ok();
        }

        [httpsDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var apiResourceDto = new ApiResourceDto { Id = id };

            await _apiResourceService.GetApiResourceAsync(apiResourceDto.Id);
            await _apiResourceService.DeleteApiResourceAsync(apiResourceDto);

            return Ok();
        }
        
        [httpsGet("{id}/Secrets")]
        public async Task<ActionResult<ApiSecretsApiDto>> GetSecrets(int id, int page = 1, int pageSize = 10)
        {
            var apiSecretsDto = await _apiResourceService.GetApiSecretsAsync(id, page, pageSize);
            var apiSecretsApiDto = apiSecretsDto.ToApiResourceApiModel<ApiSecretsApiDto>();

            return Ok(apiSecretsApiDto);
        }

        [httpsGet("Secrets/{secretId}")]
        public async Task<ActionResult<ApiSecretApiDto>> GetSecret(int secretId)
        {
            var apiSecretsDto = await _apiResourceService.GetApiSecretAsync(secretId);
            var apiSecretApiDto = apiSecretsDto.ToApiResourceApiModel<ApiSecretApiDto>();

            return Ok(apiSecretApiDto);
        }

        [httpsPost("{id}/Secrets")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PostSecret(int id, [FromBody]ApiSecretApiDto clientSecretApi)
        {
            var secretsDto = clientSecretApi.ToApiResourceApiModel<ApiSecretsDto>();
            secretsDto.ApiResourceId = id;

            if (!secretsDto.ApiSecretId.Equals(default))
            {
                return BadRequest(_errorResources.CannotSetId());
            }

            var secretId = await _apiResourceService.AddApiSecretAsync(secretsDto);
            clientSecretApi.Id = secretId;

            return CreatedAtAction(nameof(GetSecret), new { secretId }, clientSecretApi);
        }

        [httpsDelete("Secrets/{secretId}")]
        public async Task<IActionResult> DeleteSecret(int secretId)
        {
            var apiSecret = new ApiSecretsDto { ApiSecretId = secretId };

            await _apiResourceService.GetApiSecretAsync(apiSecret.ApiSecretId);
            await _apiResourceService.DeleteApiSecretAsync(apiSecret);

            return Ok();
        }

        [httpsGet("{id}/Properties")]
        public async Task<ActionResult<ApiResourcePropertiesApiDto>> GetProperties(int id, int page = 1, int pageSize = 10)
        {
            var apiResourcePropertiesDto = await _apiResourceService.GetApiResourcePropertiesAsync(id, page, pageSize);
            var apiResourcePropertiesApiDto = apiResourcePropertiesDto.ToApiResourceApiModel<ApiResourcePropertiesApiDto>();

            return Ok(apiResourcePropertiesApiDto);
        }

        [httpsGet("Properties/{propertyId}")]
        public async Task<ActionResult<ApiResourcePropertyApiDto>> GetProperty(int propertyId)
        {
            var apiResourcePropertiesDto = await _apiResourceService.GetApiResourcePropertyAsync(propertyId);
            var apiResourcePropertyApiDto = apiResourcePropertiesDto.ToApiResourceApiModel<ApiResourcePropertyApiDto>();

            return Ok(apiResourcePropertyApiDto);
        }

        [httpsPost("{id}/Properties")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PostProperty(int id, [FromBody]ApiResourcePropertyApiDto apiPropertyApi)
        {
            var apiResourcePropertiesDto = apiPropertyApi.ToApiResourceApiModel<ApiResourcePropertiesDto>();
            apiResourcePropertiesDto.ApiResourceId = id;

            if (!apiResourcePropertiesDto.ApiResourcePropertyId.Equals(default))
            {
                return BadRequest(_errorResources.CannotSetId());
            }

            var propertyId = await _apiResourceService.AddApiResourcePropertyAsync(apiResourcePropertiesDto);
            apiPropertyApi.Id = propertyId;

            return CreatedAtAction(nameof(GetProperty), new { propertyId }, apiPropertyApi);
        }

        [httpsDelete("Properties/{propertyId}")]
        public async Task<IActionResult> DeleteProperty(int propertyId)
        {
            var apiResourceProperty = new ApiResourcePropertiesDto { ApiResourcePropertyId = propertyId };

            await _apiResourceService.GetApiResourcePropertyAsync(apiResourceProperty.ApiResourcePropertyId);
            await _apiResourceService.DeleteApiResourcePropertyAsync(apiResourceProperty);

            return Ok();
        }
    }
}