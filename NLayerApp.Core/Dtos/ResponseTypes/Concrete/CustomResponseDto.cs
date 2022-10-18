using NLayerApp.Core.Dtos.ResponseTypes.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NLayerApp.Core.Dtos.ResponseTypes.Concrete
{
    public class CustomResponseDto<T> : ICustomResponseDto<T>
    {
        public T Data { get; set; }
        [JsonIgnore]
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public List<String> ErrorMessages { get; set; }
        public List<String> ValidatonErrorMessages { get; set; }

        public static CustomResponseDto<T> Success(int statusCode, T data, bool isSuccess)
        {
            return new CustomResponseDto<T> { Data = data, StatusCode = statusCode, IsSuccess = isSuccess };
        }
        public static CustomResponseDto<T> Success(int statusCode, bool isSuccess)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, IsSuccess = isSuccess };
        }

        public static CustomResponseDto<T> Fail(int statusCode, List<string> errors,
            List<string> validationErrorMessages, bool isSuccess)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, ErrorMessages = errors, ValidatonErrorMessages = validationErrorMessages, IsSuccess = isSuccess };
        }

        public static CustomResponseDto<T> Fail(int statusCode, List<string> validationErrorMessages, bool isSuccess)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, ValidatonErrorMessages = validationErrorMessages, IsSuccess = isSuccess };
        }

        public static CustomResponseDto<T> Fail(int statusCode, string error, string validationErrorMessage, bool isSuccess)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, ErrorMessages = new List<string> { error }, ValidatonErrorMessages = new List<string> { validationErrorMessage }, IsSuccess = isSuccess };
        }

        public static CustomResponseDto<T> Fail(int statusCode, string error, bool isSuccess)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, ErrorMessages = new List<string> { error }, IsSuccess = isSuccess };
        }
    }
}
