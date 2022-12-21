namespace Dealeron.SalesTaxes.Application.Common.Models
{
    public sealed class OperationResult
    {
        public dynamic? ObjectData { get; set; }
        public OperationResultEnum Status { get; set; }
        public IEnumerable<string> Errors { get; set; }

        public OperationResult() 
        {
            Status = OperationResultEnum.Unknown;
            Errors = new List<string>();
        }

        public static OperationResult CreateSucces(dynamic objectData)
        {
            return new OperationResult
            {
                Status = OperationResultEnum.Succes,
                ObjectData = objectData,
                Errors = new List<string>(),
            };
        }

        public static OperationResult CreateFail(IEnumerable<string> errors) 
        {
            return new OperationResult
            {
                Status = OperationResultEnum.Fail,
                Errors = errors,
                ObjectData = null,
            };
        }
    }
}
