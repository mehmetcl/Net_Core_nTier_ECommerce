using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ECommerce.SharedLibrary.Dtos
{
    public class CustomResponseDto <T>
    {
        public T Data { get; set; }

      
        public int StatusCode { get;  set; }

        public List<String> Errors { get; set; }

        [JsonIgnore]
        public bool IsSuccessful { get; set; }

        public static CustomResponseDto<T> Success(int statusCode, T data)
        {
            return new CustomResponseDto<T>
            {
                Data = data,
                StatusCode = statusCode,
                IsSuccessful = true,
                // Errors = null,Default Referance


            };
        }
        public static CustomResponseDto<T> Success(int statusCode,bool isShow)
        {
            return new CustomResponseDto<T>
            {
                StatusCode = statusCode,
                IsSuccessful = true,
            };
        }
        public static CustomResponseDto<T> Fail( int statusCode, List<string> errors,bool isactive)
        {
            return new CustomResponseDto<T>
            {
                
                StatusCode = statusCode,
                Errors = errors,
                 IsSuccessful = false,
            };
        }
        public static CustomResponseDto<T> Fail(int statusCode, string error,bool isactive)
        {
            return new CustomResponseDto<T>
            {
                StatusCode = statusCode,
                Errors = new List<string> { error },
                IsSuccessful = false,    
            };
        }
    }
}
