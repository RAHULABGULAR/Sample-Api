namespace Sample_Api.Models
{
    public class ServiceResponse<T>
    {
        public T data{get;set;}
        public bool success{get;set;}=true;
        public string message{get;set;}="Response";
    }
}