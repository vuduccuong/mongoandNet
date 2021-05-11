using App.Domain.Entities;
using App.Domain.IServices;
using App.Domain.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipwrecksController : ControllerBase
    {
        private readonly IShipwreckService _shipwreckService;
        private readonly IFileManagerService _fileService;
        private readonly IMemoryCache _cache;

        public ShipwrecksController(
            IShipwreckService shipwreckService,
            IFileManagerService fileService,
            IMemoryCache cache
            )
        {
            _shipwreckService = shipwreckService;
            _fileService = fileService;
            _cache = cache;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cacheKey = "test";
            if (!_cache.TryGetValue(cacheKey, out IEnumerable<ShipwreckEntity> data))
            {
                data = await _shipwreckService.GetAll();
                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromMinutes(2)
                };
                _cache.Set(cacheKey, data, cacheExpiryOptions);
            }
            return Ok(data);
        }

        
        [HttpPost("[action]")]
        [Authorize]
        public async Task<IActionResult> Uploads([FromForm] UploadManageVM frmData)
        {
            Guid myGuid = Guid.NewGuid();
            var filePath = Path.Combine("wwwroot", myGuid + frmData.File.FileName);
            var fileInfo = new FileInfo(filePath);
            if (frmData.File != null)
            {
                using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    //nén ảnh trước khi lưu
                    await frmData.File.CopyToAsync(fileStream);
                }
            }
            FileManagerEntity fileEntity = new();
            fileEntity.Path = filePath;
            await _fileService.UploadFile(fileEntity);
            return Ok(fileInfo.FullName);
        }
    }
}