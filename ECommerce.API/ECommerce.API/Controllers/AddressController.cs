﻿using AutoMapper;
using ECommerce.BusinessLayer.Abstract;
using ECommerce.EntityLayer.Concrete;
using ECommerce.EntityLayer.DTOS;
using ECommerce.SharedLibrary.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController(IMapper mapper, IAddressService addressService) : CustomBaseController
    {
        private readonly IMapper _mapper = mapper;
        private readonly IAddressService _addressService = addressService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var addresses = await _addressService.GetAllAsync();
            var addressDtos = _mapper.Map<List<AddressDto>>(addresses).ToList();

            return CreateActionResult
                (CustomResponseDto<List<AddressDto>>.Success(200, addressDtos));
        }
        [HttpGet("{addressId}")]

        public async Task<IActionResult> GetAddressById(string addressId)
        {
            var addresses = await _addressService.GetByIdAsync(addressId);
            var addressesDto = _mapper.Map<AddressDto>(addresses);
            return CreateActionResult(CustomResponseDto<AddressDto>.Success(200, addressesDto));
        }
        [HttpGet("[action]/{userId}")]
        public async Task<IActionResult> GetAddresses(string userId)
        {
            var addresses = await _addressService.GetAddressAsync(userId);
            var addressesDtos = _mapper.Map<List<AddressDto>>(addresses.ToList());
            return CreateActionResult(CustomResponseDto<List<AddressDto>>.Success(200, addressesDtos));


        }

        [HttpPost]
        public async Task<IActionResult> Save(AddressDto addressDto)
        {
            var address = await _addressService.AddAsync(_mapper.Map<Address>(addressDto));
            var addresssesDto = _mapper.Map<AddressDto>(address);
            return CreateActionResult(CustomResponseDto<AddressDto>.Success(200, addresssesDto));

        }
        [HttpDelete("{id}")]

        public async Task<IActionResult> Remove(string id)
        {
            var address = await _addressService.GetByIdAsync(id);
                await _addressService.RemoveAsync(address);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204, true));
        }
        [HttpPut]
        public async Task<IActionResult> Update(AddressDto addressDto)
        {
            await _addressService.UpdateAsync(_mapper.Map<Address>(addressDto));


            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204, true));
        }
    }
}
